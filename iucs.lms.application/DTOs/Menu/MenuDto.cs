using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iucs.lms.application.DTOs.Menu
{
    public class MenuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public Guid? ParentId { get; set; }
        public int Sequence { get; set; }
        public bool IsVisible { get; set; }
    }
}
