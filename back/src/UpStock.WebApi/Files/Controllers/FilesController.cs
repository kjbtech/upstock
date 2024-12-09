using Microsoft.AspNetCore.Mvc;

namespace UpStock.WebApi.Files.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesController : ControllerBase
{
    private readonly CloudFlareR2 _cloudFlareR2;

    public FilesController(CloudFlareR2 cloudFlareR2)
    {
        _cloudFlareR2 = cloudFlareR2;
    }

    [HttpPost]
    public async Task<IActionResult> Send(
        List<IFormFile> files,
        CancellationToken cancellationToken)
    {
        if (files == null || files.Count == 0)
            return BadRequest("No files uploaded.");

        var uploadedFiles = new List<string>();

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                var key = $"todo/{Guid.NewGuid()}_{file.FileName}"; // Unique key for the file
                using (var stream = file.OpenReadStream())
                {
                    await _cloudFlareR2.AddAsync(
                        new CloudFlareR2.FileToUpload(file.OpenReadStream(), file.FileName, file.ContentType),
                        cancellationToken);
                }

                uploadedFiles.Add(key);
            }
        }

        return Ok();
    }
}
