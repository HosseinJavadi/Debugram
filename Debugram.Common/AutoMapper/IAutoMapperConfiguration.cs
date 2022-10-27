using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugram.Common.AutoMapper
{
    public interface IAutoMapperConfiguration
    {
        IMapper Mapper();
    }
}
