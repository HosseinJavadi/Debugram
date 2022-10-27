using System;
using System.Collections.Generic;

namespace Debugram.Entities.ModelDbContext
{
    public partial class User
    {
        public User()
        {
            Posts = new HashSet<Post>();
            UserStills = new HashSet<UserStill>();
        }

        public long Id { get; set; }
        public string? UserName { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? AvatarAddress { get; set; }
        public string Password { get; set; } = null!;
        public string? Biography { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime InsertDate { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<UserStill> UserStills { get; set; }
    }
}
