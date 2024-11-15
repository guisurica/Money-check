using Money.Domain.Contracts.Bases;
using Money.Domain.Entities;

namespace Money.Domain.Contracts.Repositories
{
    public interface IUserRepository: IRepository<UserEntity>
    {
        Task<UserEntity> GetByUsername(string username);
        Task<UserEntity?> GetByEmail(string username);
    }
}
