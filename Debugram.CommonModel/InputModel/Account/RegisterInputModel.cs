using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugram.CommonModel.InputModel.Account
{
    public class RegisterInputModel:IValidatableObject
    {
        public string UserName { get; set; }
        [Required(ErrorMessage ="نام و نام خانوادگی نمیتواند خالی باشد")]
        public string FullName { get; set; }

        [EmailAddress(ErrorMessage = "ایمیل نادرست است")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> Errors = new List<ValidationResult>();
            if (Password != ConfirmPassword)
                Errors.Add(new ValidationResult("رمز عبور با تکرار رمز عبور مطابقت ندارد"));
            return Errors;
        }
    }
}
