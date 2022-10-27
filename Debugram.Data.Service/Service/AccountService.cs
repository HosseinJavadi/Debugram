using Debugram.Common.CustomeException;
using Debugram.Common.Utilities;
using Debugram.CommonModel.Enums;
using Debugram.CommonModel.InputModel.Account;
using Debugram.CommonModel.ViewModel.Account;
using Debugram.Data.Contracts;
using Debugram.Data.Service.IService.Account;
using System.Net;
using Debugram.Common.AutoMapper;
using Debugram.CommonModel.ViewModel;

namespace Debugram.Data.Service.Service
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAutoMapperConfiguration _autoMapper;

        public AccountService(IUserRepository userRepository, IAutoMapperConfiguration autoMapper)
        {
            _userRepository = userRepository;
            _autoMapper = autoMapper;
        }
        public UserViewModel UserLogin(RegisterInputModel param)
        {
            if (_userRepository.TableNoTracking.Any(n => n.Email != param.Email))
                throw new AppException(ResultApiStatusCode.NotFoundUser, ResultApiStatusCode.NotFoundUser.ToDisplay(), HttpStatusCode.BadRequest);

            Assert.NotNull<string>(param.Password, "رمز عبور", ResultApiStatusCode.BadRequest.ToDisplay());
            var passwordHash = SecurityHelper.GetSha256Hash(param.Password);
            if (!_userRepository.TableNoTracking.Any(n => n.Email == param.Email && n.Password == passwordHash))
                throw new AppException(ResultApiStatusCode.NotFoundUser, ResultApiStatusCode.NotFoundUser.ToDisplay(), HttpStatusCode.BadRequest);

            var user = _userRepository.GetUserByEmail(param.Email);

            var mapper = _autoMapper.Mapper();
            return mapper.Map<UserViewModel>(user);
        }
    }
}
