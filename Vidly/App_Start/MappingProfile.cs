using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            CreateMap<MembershipType, MembershipTypeDto>();

            CreateMap<Genre, GenreDto>();
        }
    }
}