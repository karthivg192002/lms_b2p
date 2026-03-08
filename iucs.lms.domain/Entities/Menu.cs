namespace iucs.lms.domain.Entities;

public class Menu
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public int? ParentId { get; set; }
    
    // Self-referencing navigation for submenus
    public Menu? ParentMenu { get; set; }
    public ICollection<Menu> SubMenus { get; set; } = new List<Menu>();
    
    // Navigation for roles
    public ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
}
