using Money.Domain.Contracts.Bases;
using Money.Domain.Entities;

namespace Money.Domain.DTOs.User
{
    public class UserDTO : DTO
    {
        public string Email { get; set; }
        public string Picture { get; set; }
        public int Balance { get; set; }
        public int ExpectedBudget { get; set; }
        public List<TransactionEntity> Transactions { get; set; }
    }
}
