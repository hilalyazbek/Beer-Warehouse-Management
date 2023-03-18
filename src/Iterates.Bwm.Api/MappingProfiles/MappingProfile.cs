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
        CreateMap<Brewer, BrewerDTO>().ReverseMap();
        CreateMap<Wholesaler, WholesalerDTO>().ReverseMap();
        CreateMap<Beer, AddBeerDTO>().ReverseMap();
        CreateMap<Beer, BeerDTO>().ReverseMap();
        CreateMap<Sale, AddSaleDTO>().ReverseMap();
        CreateMap<Sale, ViewSaleDTO>().ReverseMap();
        CreateMap<QuotationRequest, QuotationRequestDTO>().ReverseMap();
        CreateMap<QuotationResponse, QuotationResponseDTO>().ReverseMap();
        CreateMap<ItemRequest, ItemRequestDTO>().ReverseMap();
        CreateMap<ItemResponse, ItemResponseDTO>().ReverseMap();
        CreateMap<WholesalerStock, WholesalerStockDTO>().ReverseMap();
    }
}
