using Money.Domain.Contracts.Bases;
using Money.Domain.DTOs.User;
using Money.Domain.Helpers;

namespace Money.Domain.Contracts.Services
{
    public interface IUserService : IService<UserDTO, CreateUserDTO, UpdateUserDTO>
    {
        Task<Result<UserDTO>> Login(LoginUserDTO input);
    }
}
