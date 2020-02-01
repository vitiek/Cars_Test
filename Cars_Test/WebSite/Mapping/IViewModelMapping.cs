using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Mapping
{
    interface IViewModelMapping
    {
        void Create(IMapperConfigurationExpression configuration);
    }
}
