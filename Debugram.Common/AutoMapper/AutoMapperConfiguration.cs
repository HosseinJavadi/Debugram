using AutoMapper;
using Debugram.CommonModel.ViewModel;
using Debugram.Entities.ModelDbContext;


namespace Debugram.Common.AutoMapper
{
    public class AutoMapperConfiguration: IAutoMapperConfiguration
    {
        private readonly MapperConfiguration _mapperConfiguration;
        public AutoMapperConfiguration()
        {
            _mapperConfiguration = new MapperConfiguration(n =>
            {
                n.CreateMap<UserViewModel, User>();
                n.CreateMap<User, UserViewModel>();
            });
        }

        public  IMapper Mapper()
        {
            return _mapperConfiguration.CreateMapper();
        }
    }
}
