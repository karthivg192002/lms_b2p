using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iucs.lms.application.DTOs
{
    public class BookUploadRequest
    {
        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsWatermarkRequired { get; set; }

        public bool IsDownloadable { get; set; }

        public IFormFile BookFile { get; set; }

        public IFormFile CoverImage { get; set; }
    }
}