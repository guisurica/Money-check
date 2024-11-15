namespace Money.Domain.Contracts.Bases
{
    public interface IRepository<T> where T : Entity
    {
        Task<bool> Create(T entity);
    }
}
