using System;
using System.Collections.Generic;

namespace Debugram.Entities.ModelDbContext
{
    public partial class Still
    {
        public Still()
        {
            UserStills = new HashSet<UserStill>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<UserStill> UserStills { get; set; }
    }
}
