using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class BoardBase : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class Board : BoardBase
{
    public ICollection<Class> Classes { get; set; } = new List<Class>();
}
