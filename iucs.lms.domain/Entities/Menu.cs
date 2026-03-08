using iucs.lms.domain.Entities.Common;

namespace iucs.lms.domain.Entities;

public class MenuBase : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public Guid? ParentId { get; set; }
    public int Sequence { get; set; }
    public bool IsVisible { get; set; } = true;
}

public class Menu : MenuBase
{
    public Menu? ParentMenu { get; set; }
    public ICollection<Menu> SubMenus { get; set; } = new List<Menu>();
    public ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
}
