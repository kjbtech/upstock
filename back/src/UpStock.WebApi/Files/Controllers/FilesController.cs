using Microsoft.AspNetCore.Mvc;

namespace UpStock.WebApi.Files.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesController : ControllerBase
{
    private readonly CloudFlareR2 _cloudFlareR2;
    private readonly GetItemsFromFile _addItemsFromFile;

    public FilesController(CloudFlareR2 cloudFlareR2, GetItemsFromFile addItemsFromFile)
    {
        _cloudFlareR2 = cloudFlareR2;
        _addItemsFromFile = addItemsFromFile;
    }

    [HttpPost]
    public async Task<IActionResult> Send(
        List<IFormFile> files,
        CancellationToken cancellationToken)
    {
        if (files == null || files.Count == 0)
        {
            return BadRequest("No files uploaded.");
        }

        var uploadedFiles = new List<string>();

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var fileKey = await _cloudFlareR2.AddAsync(
                        new CloudFlareR2.FileToUpload(file.OpenReadStream(), file.FileName, file.ContentType),
                        cancellationToken);

                    if (fileKey?.Length > 0)
                    {
                        uploadedFiles.Add(fileKey);
                        await _addItemsFromFile.ExecuteAsync(
                            new FileToAnalyze(CloudFlareR2.GetPublicUri(fileKey))
                            );
                    }
                }
            }
        }

        return Ok();
    }
}
