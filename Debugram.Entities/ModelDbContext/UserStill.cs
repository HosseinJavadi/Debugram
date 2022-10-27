using System;
using System.Collections.Generic;

namespace Debugram.Entities.ModelDbContext
{
    public partial class UserStill
    {
        public long UserId { get; set; }
        public int StillId { get; set; }
        public byte StillAmount { get; set; }
        public byte Rating { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual Still Still { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
