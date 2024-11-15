using Money.Domain.DTOs.User;
using Money.Domain.Entities;

namespace Money.Tests.UnitTests.Fixtures
{
    public static class UserFixture
    {
        public static CreateUserDTO CreateUserDTO = new() { 
            ConfirmPassword = "123123Jhon@",
            Password = "123123Jhon@",
            Name = "Jhon Doe",
            Email = "JhonDoe@email.com"
        };

        public static CreateUserDTO CreateUserDTOWithInvalidEmail = new()
        {
            ConfirmPassword = "123123",
            Password = "123123",
            Name = "Jhon Doe",
            Email = "JhonDoe"
        };

        public static CreateUserDTO CreateUserDTOWithInvalidUsername = new()
        {
            ConfirmPassword = "123123",
            Password = "123123",
            Name = "",
            Email = "JhonDoe@email.com"
        };

        public static CreateUserDTO CreateUserDTOWithPasswordDoesntMatching = new()
        {
            ConfirmPassword = "1231234",
            Password = "123123",
            Name = "Jhon Doe",
            Email = "JhonDoe@email.com"
        };

        public static CreateUserDTO CreateUserDTOWithPasswordIsInvalid = new()
        {
            ConfirmPassword = "123123",
            Password = "123123",
            Name = "Jhon Doe",
            Email = "JhonDoe@email.com"
        };

        public static UserDTO UserCreatedSuccessfully = new() { 
            Balance = 0,
            ExpectedBudget = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Email = "JhonDoe@email.com",
            Id = "a689b78daa104269b08a36075585bf95",
            Name = "Jhon Doe",
            Picture = "emp",
            Transactions = new List<TransactionEntity>()
        };

        public static UserEntity UserEntity = new() {
            Balance = 0,
            ExpectedBudget = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Email = "JhonDoe@email.com",
            Id = string.Empty,
            Name = "Jhon Doe",
            Picture = string.Empty,
            Transactions = new List<TransactionEntity>(),
            Indexes = new List<string>(),
            Password = "123123Jhon@"
        };
    }
}
