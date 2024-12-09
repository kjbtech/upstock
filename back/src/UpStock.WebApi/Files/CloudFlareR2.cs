using Amazon.S3.Transfer;
using Amazon.S3;

namespace UpStock.WebApi.Files;

/// <summary>
/// We are using AWS S3 SDK thanks to CloudFlare documentation.
/// See https://developers.cloudflare.com/r2/examples/aws/aws-sdk-net/
/// </summary>
public class CloudFlareR2
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName = "upstock";
    private static string _publicDomain = "https://r2.kjbconseil.dev";

    public CloudFlareR2(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    public async Task<string> AddAsync(
        FileToUpload fileToUpload,
        CancellationToken cancellationToken)
    {
        if (fileToUpload.FileStream.Length > 0)
        {
            var key = $"{Guid.NewGuid()}_{fileToUpload.FileName}"; // Unique key for the file
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = fileToUpload.FileStream,
                Key = key,
                BucketName = _bucketName,
                ContentType = fileToUpload.ContentType,
                DisablePayloadSigning = true // mandatory when using AWS S3 SDK with CloudFlare R2
            };

            var transferUtility = new TransferUtility(_s3Client);
            await transferUtility.UploadAsync(uploadRequest, cancellationToken);

            return key;
        }
        return string.Empty;
    }

    public static Uri GetPublicUri(string key)
    {
        return new Uri(new Uri(_publicDomain), key);
    }

    public record FileToUpload(Stream FileStream, string FileName, string ContentType);
}
