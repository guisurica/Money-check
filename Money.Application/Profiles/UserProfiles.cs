using AutoMapper;
using Money.Domain.DTOs.User;
using Money.Domain.Entities;

namespace Money.Application.Profiles
{
    public  class UserProfiles : Profile
    {
        public UserProfiles() 
        {
            CreateMap<CreateUserDTO, UserEntity>();
            CreateMap<UserEntity, UserDTO>();
        }
    }
}
