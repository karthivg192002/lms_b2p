using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class BookBase : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string CoverImageUrl { get; set; } = string.Empty;
    public bool IsWatermarkRequired { get; set; }
    public bool IsDownloadable { get; set; }
    public DateTime UploadedAt { get; set; }
}

public class Book : BookBase
{
    
}