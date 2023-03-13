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
    }
}
