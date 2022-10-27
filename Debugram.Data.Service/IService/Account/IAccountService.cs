using Debugram.CommonModel.InputModel.Account;
using Debugram.CommonModel.ViewModel;
using Debugram.CommonModel.ViewModel.Account;


namespace Debugram.Data.Service.IService.Account
{
    public interface IAccountService
    {
        UserViewModel UserLogin(RegisterInputModel param);
    }
}
