using AutoMapper;
using MyWebApi.Database;
using MyWebApi.DTOs;

namespace MyWebApi.Configutations
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Student,StudentDTO>().ReverseMap();
        }
    }
}