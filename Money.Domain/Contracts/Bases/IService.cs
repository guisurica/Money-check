using Money.Domain.Helpers;

namespace Money.Domain.Contracts.Bases
{
    public interface IService<TResult, TCreate, TUpdate>
    {
        Task<Result<TResult>> Create(TCreate input);
        Task<Result<TResult>> Update(TUpdate input);
        Task<Result<TResult>> Read(string id);
        Task<Result<bool>> Delete(string id);
    }
}
