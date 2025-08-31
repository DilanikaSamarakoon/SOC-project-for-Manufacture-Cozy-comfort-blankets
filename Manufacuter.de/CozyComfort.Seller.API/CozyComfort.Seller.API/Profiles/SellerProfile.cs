using AutoMapper;
using CozyComfort.Seller.API.DTOs;
using Model = CozyComfort.Seller.API.Models; // <-- USE AN ALIAS FOR THE MODELS NAMESPACE

namespace CozyComfort.Seller.API.Profiles
{
    public class SellerProfile : Profile
    {
        public SellerProfile()
        {
            // --- Seller Mappings ---
            // Use the "Model" alias to be specific
            


            // --- Order Mappings ---
            // Use the "Model" alias to be specific
            CreateMap<Model.SellerOrder, SellerOrderDto>();
            CreateMap<Model.SellerOrderItem, SellerOrderItemDto>();
            CreateMap<CreateSellerOrderDto, Model.SellerOrder>()
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore());
            CreateMap<CreateSellerOrderItemDto, Model.SellerOrderItem>();
            CreateMap<Model.SellerStock, SellerStockDto>();
            CreateMap<Model.Seller, SellerDto>();
           // CreateMap<CreateSellerDto, Model.Seller>();
        }
    }
}