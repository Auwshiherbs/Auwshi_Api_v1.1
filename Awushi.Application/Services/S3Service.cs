using Amazon.S3;
using Amazon.S3.Transfer;
using Awushi.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Application.Services
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;

        public S3Service(IAmazonS3 s3Client, IConfiguration configuration )
        {
            _s3Client = s3Client;
            _configuration = configuration;
        }
        public Task DeleteFileAsync(string keyName)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetFileAsync(string keyName)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var bucketName = _configuration["AwsConfiguration:AwsBucketName"];
            var keyName = Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);

            var newMemoryStream = new MemoryStream();
            file.CopyTo(newMemoryStream);

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = new MemoryStream(),
                Key = keyName,
                BucketName = bucketName,
               
            };

            var fileTransferUtility = new TransferUtility(_s3Client);
            await fileTransferUtility.UploadAsync(uploadRequest);


           return keyName;
        }
    }
}
