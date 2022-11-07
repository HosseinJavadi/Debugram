using Debugram.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Debugram.CommonModel.ViewModel
{
    public class UserViewModel
    {
        [JsonIgnore]
        public long Id { get; set; }

        public string UserId
        {
            get
            {
                return SecurityHelper.Encrypt(Id.ToString(), "Senator0012");
            }
        }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string UserName { get; set; }

        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? AvatarAddress { get; set; }
        [JsonIgnore]
        public string Password { get; set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? Biography { get; set; }
        [JsonIgnore]
        public bool? IsActive { get; set; }
        public Guid? SecurityStamp { get; set; }
        [JsonIgnore]

        public bool IsDelete { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
