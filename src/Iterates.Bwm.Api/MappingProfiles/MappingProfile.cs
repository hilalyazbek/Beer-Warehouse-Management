using AutoMapper;
using DnsClient;
using Iterates.Bwm.Api.DTOs;
using Iterates.Bwm.Domain.Entities;
using System;

namespace Iterates.Bwm.Api.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Beer, AddBeerDTO>().ReverseMap();
        CreateMap<Sale, AddSaleDTO>().ReverseMap();
        CreateMap<QuotationRequest, QuotationRequestDTO>().ReverseMap();
        CreateMap<QuotationResponse, QuotationResponseDTO>().ReverseMap();
        CreateMap<ItemRequest, ItemRequestDTO>().ReverseMap();
        CreateMap<ItemResponse, ItemResponseDTO>().ReverseMap();
        CreateMap<Wholesaler, WholesalerDTO>().ReverseMap();
    }
}
