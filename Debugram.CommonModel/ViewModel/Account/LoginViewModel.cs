using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugram.CommonModel.ViewModel.Account
{
    public class LoginViewModel
    {
        public string Token { get; set; } = null!;
        public UserViewModel User { get; set; } = null!;

    }
}
