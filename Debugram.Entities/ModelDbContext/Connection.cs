using System;
using System.Collections.Generic;

namespace Debugram.Entities.ModelDbContext
{
    public partial class Connection
    {
        public long Id { get; set; }
        public long Follower { get; set; }
        public long Following { get; set; }
        public bool? IsFllow { get; set; }
    }
}
