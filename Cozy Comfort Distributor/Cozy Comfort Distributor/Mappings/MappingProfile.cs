using AutoMapper;
using Cozy_Comfort_Distributor.Models; // Your new project's models
using Cozy_Comfort_Distributor.Dtos;   // Your new project's DTOs

namespace Cozy_Comfort_Distributor.Mappings // Your project's Mappings namespace
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Distributor Mappings
            CreateMap<Distributor, DistributorDto>();
            CreateMap<CreateDistributorDto, Distributor>();

            // DistributorOrder Mappings
            CreateMap<DistributorOrder, DistributorOrderDto>()
                .ForMember(dest => dest.DistributorName, opt => opt.MapFrom(src => src.Distributor!.Name));
            CreateMap<CreateDistributorOrderDto, DistributorOrder>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items)); // Map nested items

            // DistributorOrderItem Mappings
            CreateMap<DistributorOrderItem, DistributorOrderItemDto>();
            CreateMap<CreateDistributorOrderItemDto, DistributorOrderItem>();
        }
    }
}