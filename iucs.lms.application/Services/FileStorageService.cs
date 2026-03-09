using Amazon.S3;
using Amazon.S3.Transfer;
using iucs.lms.domain.Entities;
using iucs.lms.domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace iucs.lms.application.Services
{
    public interface IFileStorageService
    {
        Task<string> UploadLocalFileAsync(IFormFile file, string folder);

        Task<string> UploadAWSFileAsync(IFormFile file, string folder);

        Task<Book> AddBookAsync(Book book);
    }

    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;
        private readonly IRepository<Book> _repository;

        public FileStorageService(IWebHostEnvironment env, IConfiguration config, IRepository<Book> repository)
        {
            _env = env;
            _config = config;
            _repository = repository;
        }

        public async Task<string> UploadLocalFileAsync(IFormFile file, string folder)
        {
            string folderPath = Path.Combine(_env.WebRootPath, folder);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            string filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(stream);

            return "/" + folder + "/" + fileName;
        }

        public async Task<string> UploadAWSFileAsync(IFormFile file, string folder)
        {
            var bucket = _config["AWS:BucketName"];

            var client = new AmazonS3Client(
                _config["AWS:AccessKey"],
                _config["AWS:SecretKey"],
                Amazon.RegionEndpoint.APSouth1
            );

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            using var stream = file.OpenReadStream();

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = stream,
                BucketName = bucket,
                Key = $"{folder}/{fileName}",
                ContentType = file.ContentType
            };

            var transferUtility = new TransferUtility(client);

            await transferUtility.UploadAsync(uploadRequest);

            return $"https://{bucket}.s3.amazonaws.com/{folder}/{fileName}";
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            var result = _repository.AddAsync(book);
            return book;
        }
    }
}