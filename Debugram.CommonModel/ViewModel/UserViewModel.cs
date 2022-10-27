using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugram.CommonModel.ViewModel
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? AvatarAddress { get; set; }
        public string Password { get; set; } = null!;
        public string? Biography { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
