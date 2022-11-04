using Debugram.Common.CustomeException;
using Debugram.Common.Enums;

namespace Debugram.Common.Utilities
{
    public static class ObjectChecker
    {
        #region Characters
        private static List<string> SpecialCharacters = new List<string>()
        {
            "!",
            "@",
            "#",
            "$",
            "%",
            "^",
            "&",
            "*",
            "(",
            ")",
            "-",
            "_",
            "=",
            "+",
            "*",
            "/",
            "<",
            ">",
        };
        private static List<string> Numbers = new List<string>()
        {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
        };
        #endregion
        public static bool CheckPassword(string password, bool hasSpecialCharacter = true, bool hasNumber = true, bool throwEx = true)
        {
            if (throwEx)
                Assert.NotNull<string>(password, "رمز عبور", "رمز عبور خالی میباشد");

            if (password.Length < 6)
            {
                if (throwEx)
                    throw new AppException(ResultApiStatusCode.PasswordLength,
                        ResultApiStatusCode.PasswordLength.ToDisplay(),
                        System.Net.HttpStatusCode.BadRequest);

                return false;
            }
            if(hasSpecialCharacter)
            {
                if (!SpecialCharacters.Any(n => password.Contains(n)))
                {
                    if (throwEx)
                        throw new AppException(ResultApiStatusCode.PsaswordNotValidSpecialCharacters,
                        ResultApiStatusCode.PsaswordNotValidSpecialCharacters.ToDisplay(),
                        System.Net.HttpStatusCode.BadRequest);
                    else
                        return false;


                }
            }
            if (hasNumber)
            {
                if(!Numbers.Any(n => password.Contains(n)))
                {
                    if (throwEx)
                        throw new AppException(ResultApiStatusCode.PsaswordNotValidNumber,
                        ResultApiStatusCode.PsaswordNotValidNumber.ToDisplay(),
                        System.Net.HttpStatusCode.BadRequest);
                    else
                        return false;
                }
            }

            return true;
        }
    }
}
