using System;
using System.Collections.Generic;

namespace Debugram.Entities.ModelDbContext
{
    public partial class Menu
    {
        public Menu()
        {
            InverseParent = new HashSet<Menu>();
        }

        public long Id { get; set; }
        public string? Url { get; set; }
        public int PostId { get; set; }
        public long? ParentId { get; set; }
        public string Title { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }

        public virtual Menu? Parent { get; set; }
        public virtual Post Post { get; set; } = null!;
        public virtual Article? Article { get; set; }
        public virtual ICollection<Menu> InverseParent { get; set; }
    }
}
