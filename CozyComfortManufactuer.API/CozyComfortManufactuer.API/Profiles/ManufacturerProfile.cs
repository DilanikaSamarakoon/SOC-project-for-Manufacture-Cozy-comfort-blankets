using CozyComfort.Manufacturer.API.Models;
using AutoMapper;
using CozyComfort.Manufacturer.API.DTO;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CozyComfort.Manufacturer.API.Profiles
{
    public class ManufacturerProfile : Profile
    {
        public ManufacturerProfile()
        {
            // Map from a create DTO to the main Blanket model
            CreateMap<BlanketCreateDto, Blanket>();

            CreateMap<OrderCreateDto, Order>();

            CreateMap<StockUpdateDto,Stock>();
        }
    }
}