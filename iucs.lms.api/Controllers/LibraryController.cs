using iucs.lms.application.DTOs;
using iucs.lms.application.Services;
using iucs.lms.domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace iucs.lms.api.Controllers
{
    [ApiController]
    [Route("api/library")]
    public class LibraryController : ControllerBase
    {
        private readonly IFileStorageService _storage;
        private readonly IConfiguration _config;

        public LibraryController(IFileStorageService storage, IConfiguration config)
        {
            _storage = storage;
            _config = config;
        }

        [HttpPost("upload-book")]
        public async Task<IActionResult> UploadBook([FromForm] BookUploadRequest request)
        {
            if (request.BookFile == null)
                return BadRequest("Book file is required");

            var storageType = _config["Storage:StorageType"];

            string bookUrl = null;
            string coverUrl = null;

            if (storageType == "Local")
            {
                bookUrl = await _storage.UploadLocalFileAsync(request.BookFile, "books");

                if (request.CoverImage != null)
                {
                    coverUrl = await _storage.UploadLocalFileAsync(request.CoverImage, "covers");
                }
            }
            else if (storageType == "S3")
            {
                bookUrl = await _storage.UploadAWSFileAsync(request.BookFile, "books");

                if (request.CoverImage != null)
                {
                    coverUrl = await _storage.UploadAWSFileAsync(request.CoverImage, "covers");
                }
            }

            Book book = new Book
            {
                Title = request.Title,
                Author = request.Author,
                Description = request.Description,
                FileUrl = bookUrl,
                CoverImageUrl = coverUrl,
                IsWatermarkRequired = request.IsWatermarkRequired,
                IsDownloadable = request.IsDownloadable,
                UploadedAt = DateTime.UtcNow
            };

            await _storage.AddBookAsync(book);

            return Ok(book);
        }
    }
}