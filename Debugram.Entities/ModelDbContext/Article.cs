using System;
using System.Collections.Generic;

namespace Debugram.Entities.ModelDbContext
{
    public partial class Article
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public byte Rating { get; set; }
        public DateTime InsertDate { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }

        public virtual Menu IdNavigation { get; set; } = null!;
    }
}
