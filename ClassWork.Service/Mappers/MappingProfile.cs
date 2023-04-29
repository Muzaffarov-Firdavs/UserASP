using AutoMapper;
using ClassWork.Domain.Entites;
using ClassWork.Service.DTOs.Users;

namespace ClassWork.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // user 
            CreateMap<User, UserForCreationDto>().ReverseMap();
            CreateMap<User, UserForResultDto>().ReverseMap();
            CreateMap<User, UserForUpdateDto>().ReverseMap();
            CreateMap<UserForCreationDto, UserForUpdateDto>().ReverseMap();
            CreateMap<UserImage, UserImageForCreationDto>().ReverseMap();
            CreateMap<UserImage, UserImageForResultDto>().ReverseMap();
        }
    }
}
