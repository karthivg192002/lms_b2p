namespace iucs.lms.domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // S3 or local path to the PDF
    public string FileUrl { get; set; } = string.Empty;
    public string CoverImageUrl { get; set; } = string.Empty;
    
    // Security flags
    public bool IsWatermarkRequired { get; set; } = true;
    public bool IsDownloadable { get; set; } = false;
    
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
