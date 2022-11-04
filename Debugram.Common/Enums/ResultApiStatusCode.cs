using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugram.Common.Enums
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

        [Display(Name = "دسترسی ندارید")]
        UnAuthorized = 6,

        [Display(Name = "شماره همراه وارد شده قبلا ثبت نام شده")]
        HasPhoneNumber = 7,

        [Display(Name = "رمز عبور یا ایمیل اشتباه میباشد")]
        MistakeEmailOrPassword = 8,

        [Display(Name = "شماره همراه یا تلفن همراه نباید خالی باشد")]
        EmptyPhoneOrPassword = 9,


        [Display(Name = "کد ارسالی اشتباه میباشد")]
        CodeNotMatch = 10,

        [Display(Name = "ایمیل وارد شده تکراری می باشد")] // Confilict Email
        ExistEmail = 11,

        [Display(Name = "ایمیل وجود ندارد")]// Email Not Found 
        EmailNotFound = 12,

        [Display(Name = "کاربری وجود ندارد")]// User Not Found
        NotFoundUser = 13,

        [Display(Name = "ایمیل وارد شده قبلا ثبت نام کرده است")]// Has Email In Database :(
        HasEmailWhenRegister = 14,

        [Display(Name = "رمز عبور کمتر از 6 کاراکتر میباشد")]// Password < 6 Error (:
        PasswordLength = 15,


        [Display(Name = "مقادیر ارسالی نا معتبر میباشد")]
        InValidParameters = 16,

        [Display(Name = "رمز عبور باید دارای کارکتر خاص باشد")]
        PsaswordNotValidSpecialCharacters = 17,

        [Display(Name = "رمز عبور باید دارای عدد باشد")]
        PsaswordNotValidNumber = 18,
    }
}
