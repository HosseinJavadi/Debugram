using Debugram.Common.CustomeException;
using Debugram.Common.Utilities;
using Debugram.Data.Contracts;
using Debugram.Data.Service.IService.Account;
using System.Net;
using Debugram.CommonModel.ViewModel;
using Debugram.Entities.ModelDbContext;
using Debugram.CommonModel.InputModel.Account;
using Debugram.Common.Enums;
using Debugram.CommonModel.AutoMapper;

namespace Debugram.Data.Service.Service.Account
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

        public async Task<UserViewModel> RegisterUser(RegisterInputModel param , CancellationToken cancellationToken)
        {
            if (_userRepository.TableNoTracking.Any(n => n.Email == param.Email))
                throw new AppException(ResultApiStatusCode.HasEmailWhenRegister , ResultApiStatusCode.HasEmailWhenRegister.ToDisplay(), HttpStatusCode.BadRequest);

            ObjectChecker.CheckPassword(param.Password,false,false);
            var passwordHash = SecurityHelper.GetSha256Hash(param.Password);
            param.Password = passwordHash;
            var mapper = _autoMapper.Mapper();
            var user = mapper.Map<User>(param);
            user.SecurityStamp = Guid.NewGuid();
            await _userRepository.AddAsync(user, cancellationToken);
            var result = mapper.Map<UserViewModel>(user);
            return result;
        }

        public UserViewModel UserLogin(LoginInputModel param)
        {
            if (!_userRepository.TableNoTracking.Any(n => n.Email == param.Email))
                throw new AppException(ResultApiStatusCode.NotFoundUser, ResultApiStatusCode.NotFoundUser.ToDisplay(), HttpStatusCode.BadRequest);

            Assert.NotNull<string>(param.Password, "رمز عبور", ResultApiStatusCode.BadRequest.ToDisplay());
            var passwordHash = SecurityHelper.GetSha256Hash(param.Password);
            if (!_userRepository.TableNoTracking.Any(n => n.Email == param.Email && n.Password == passwordHash))
                throw new AppException(ResultApiStatusCode.MistakeEmailOrPassword, ResultApiStatusCode.MistakeEmailOrPassword.ToDisplay(), HttpStatusCode.BadRequest);

            var user = _userRepository.GetUserByEmail(param.Email);

            var mapper = _autoMapper.Mapper();
            return mapper.Map<UserViewModel>(user);
        }
    }
}
