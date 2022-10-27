using System;
using System.Collections.Generic;

namespace Debugram.Entities.ModelDbContext
{
    public partial class Post
    {
        public Post()
        {
            Menus = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public long UserId { get; set; }
        public int? StillTypeId { get; set; }

        public virtual StillType? StillType { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Menu> Menus { get; set; }
    }
}
