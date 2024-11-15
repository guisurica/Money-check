using Money.Domain.Contracts.Bases;

namespace Money.Domain.Entities
{
    public class UserEntity : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Picture { get; set; }
        public int Balance { get; set; }
        public int ExpectedBudget { get; set; }
        public List<TransactionEntity> Transactions { get; set; }
    }
}
