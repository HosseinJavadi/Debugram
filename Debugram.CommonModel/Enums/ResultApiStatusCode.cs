using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugram.CommonModel.Enums
{
    public enum ResultApiStatusCode
    {
        [Display(Name = "عملیات با موفقیت انجام شد")]
        Success = 0,

        [Display(Name = "خطایی در سرور رخ داده است")]
        ServerError = 1,

        [Display(Name = "پارامتر های ارسالی معتبر نیستند")]
        BadRequest = 2,

        [Display(Name = "یافت نشد")]
        NotFound = 3,

        [Display(Name = "لیست خالی است")]
        ListEmpty = 4,

        [Display(Name = "خطایی در پردازش رخ داد")]
        LogicError = 5,

        [Display(Name = "خطای احراز هویت")]
        UnAuthorized = 6,

        [Display(Name = "شماره همراه وارد شده قبلا ثبت نام شده")]
        HasPhoneNumber = 7,

        [Display(Name = "رمز عبور یا شماره همراه اشتباه میباشد")]
        MistakePhoneOrPassword = 8,

        [Display(Name = "شماره همراه یا تلفن همراه نباید خالی باشد")]
        EmptyPhoneOrPassword = 9,


        [Display(Name = "کد ارسالی اشتباه میباشد")]
        CodeNotMatch = 10
    }
}
