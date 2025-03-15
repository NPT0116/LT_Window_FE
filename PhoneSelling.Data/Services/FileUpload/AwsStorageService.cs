using PhoneSelling.Data.Models;
using Amazon.S3.Util;
using PhoneSelling.Data.Configurations;
using Amazon.S3;
using PhoneSelling.DependencyInjection;
using Amazon.S3.Model;
using PutObjectRequest = Amazon.S3.Model.PutObjectRequest;

namespace PhoneSelling.Data.Services.FileUpload
{
    public class AwsS3StorageService : IUploadService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly AWSSettings _awsSettings;

        public AwsS3StorageService()
        {
            _s3Client = DIContainer.GetKeyedSingleton<IAmazonS3>();
            var configService = DIContainer.GetKeyedSingleton<IConfigService>();
            _awsSettings = configService.GetSection<AWSSettings>("AWS");
        }

        public async Task<bool> DeleteFileAsync(string fileKey, CancellationToken cancellationToken)
        {
            var bucketName = _awsSettings.BucketName;
            var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists) return false;
            await _s3Client.DeleteObjectAsync(bucketName, fileKey);
            return true;
        }

        public async Task<Media> UploadFileAsync(MediaUploadRequest request, CancellationToken cancellationToken)
        {
            var bucketName = _awsSettings.BucketName;
            var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists) throw new Exception("Bucket name does not exist");
            var fileKey = Media.GetFileKey(request.Prefix, request.FileName);
            var putObjectRequestrequest = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = fileKey,
                InputStream = new MemoryStream(request.FileData),
                ContentType = request.ContentType,
                CannedACL = S3CannedACL.PublicRead
            };
            await _s3Client.PutObjectAsync(putObjectRequestrequest);

            var fileUrl = $"https://{bucketName}.s3.amazonaws.com/{fileKey}";
            return new Media(fileKey, string.Empty,fileUrl, MediaType.Image, null);
        }
    }
}
