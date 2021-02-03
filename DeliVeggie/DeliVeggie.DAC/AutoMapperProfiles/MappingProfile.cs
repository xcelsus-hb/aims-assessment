using AutoMapper;
using DeliVeggie.DAC.DO;
using DeliVeggie.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliVeggie.DAC.AutoMapperProfiles
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
        

    }
}
