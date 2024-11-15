using Money.Domain.Contracts.Bases;
using Money.Domain.DTOs.User;

namespace Money.Domain.Contracts.Services
{
    public interface IUserService : IService<UserDTO, CreateUserDTO, UpdateUserDTO>
    {
    }
}
