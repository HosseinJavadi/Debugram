using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugram.CommonModel.InputModel.Account
{
    public  class LoginInputModel
    {
        [EmailAddress(ErrorMessage ="ایمیل وارد شده اشتباه اشتباه است")]
        public string Email { get; set; }
        public string Password { get; set; }


    }
}
