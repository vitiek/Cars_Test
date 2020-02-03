using AutoMapper;
using BusinessLogic.Models;
using System;
using WebSite.Mapping;
using WebSite.ViewModels;

namespace WebSite.Managers
{
    public class Mapping : IViewModelMapping
    {
        public void Create(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Car, CarViewModel>(MemberList.None)
                .ForMember(c => c.Year, mapper => mapper.MapFrom(m => m.Year.Year))
                .ForMember(c => c.Manufacturer, mappper => mappper.MapFrom(m => m.Manufacturer.ToString()));

            configuration.CreateMap<CarViewModel, Car>(MemberList.None)
                .ForMember(c => c.Year, mapper => mapper.MapFrom(m => new DateTime(m.Year.Value, 1, 1)))
                .ForMember(c => c.Manufacturer, mappper => mappper.MapFrom(m => (ManufacturerEnum)Enum.Parse(typeof(ManufacturerEnum), m.Manufacturer)));
        }
    }
}
