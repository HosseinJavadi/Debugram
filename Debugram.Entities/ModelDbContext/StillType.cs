using System;
using System.Collections.Generic;

namespace Debugram.Entities.ModelDbContext
{
    public partial class StillType
    {
        public StillType()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
