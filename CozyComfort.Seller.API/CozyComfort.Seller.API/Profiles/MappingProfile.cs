using AutoMapper;
using CozyComfort.Seller.API.DTOs;
using CozyComfort.Seller.API.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Keep this map for returning seller data
        CreateMap<Seller, SellerDto>();

        // Add this new map for creating a seller
        CreateMap<CreateSellerDto, Seller>();
    }
}