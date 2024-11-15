using AutoMapper;
using Money.Application.Common;
using Money.Application.Services;
using Money.Domain.Contracts.Repositories;
using Money.Domain.DTOs.User;
using Money.Domain.Entities;
using Money.Domain.Helpers;
using Money.Tests.UnitTests.Fixtures;
using Moq;

namespace Money.Tests.UnitTests.System.Services.UserServices
{
    public class LoginUserServiceTests
    {
        [Fact]
        public async Task LoginUserIfSuccessReturns200Ok()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userService = new UserService(mockUserRepository.Object, mockMapper.Object);

            mockUserRepository
                .Setup(x => x.GetByEmail(UserFixture.LoginUserDTO.Email))
                .ReturnsAsync(UserFixture.UserEntity);

            mockMapper
                .Setup(x => x.Map<UserDTO>(UserFixture.UserEntity))
                .Returns(UserFixture.UserCreatedSuccessfully);

            var result = await userService.Login(UserFixture.LoginUserDTO);

            var resultType = Assert.IsType<Result<UserDTO>>(result);
            mockUserRepository.Verify(x => x.GetByEmail(UserFixture.LoginUserDTO.Email), Times.Once);
            mockMapper.Verify(x => x.Map<UserDTO>(UserFixture.UserEntity), Times.Once);
            Assert.True(resultType.IsSuccess);
            Assert.NotNull(resultType.Value);
            Assert.Equal("User logged successfully.", resultType.Message);
            Assert.Equal(200, resultType.Code);
        }

        [Fact]
        public async Task LoginUserIfEmailInvalidReturns400BadRequest()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userService = new UserService(mockUserRepository.Object, mockMapper.Object);

            var result = await userService.Login(UserFixture.LoginUserDTOWithInvalidEmail);

            var resultType = Assert.IsType<Result<UserDTO>>(result);
            Assert.False(resultType.IsSuccess);
            Assert.Null(resultType.Value);
            Assert.Equal("Email invalid", resultType.Message);
            Assert.Equal(400, resultType.Code);
        }

        [Fact]
        public async Task LoginUserIfPasswordInvalidReturns400BadRequest()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userService = new UserService(mockUserRepository.Object, mockMapper.Object);

            var result = await userService.Login(UserFixture.LoginUserDTOWithInvalidPassword);

            var resultType = Assert.IsType<Result<UserDTO>>(result);
            Assert.False(resultType.IsSuccess);
            Assert.Null(resultType.Value);
            Assert.Equal("Password invalid", resultType.Message);
            Assert.Equal(400, resultType.Code);
        }

        [Fact]
        public async Task LoginUserIfUseNotFoundReturns404()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userService = new UserService(mockUserRepository.Object, mockMapper.Object);

            var result = await userService.Login(UserFixture.LoginUserDTO);

            var resultType = Assert.IsType<Result<UserDTO>>(result);
            Assert.False(resultType.IsSuccess);
            Assert.Null(resultType.Value);
            Assert.Equal("User not found. Consider create a new account", resultType.Message);
            Assert.Equal(404, resultType.Code);
        }

        [Fact]
        public async Task LoginUserIfPasswordDoesntValidateReturns400()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userService = new UserService(mockUserRepository.Object, mockMapper.Object);

            mockUserRepository
                .Setup(x => x.GetByEmail(UserFixture.LoginUserDTO.Email))
                .ReturnsAsync(UserFixture.UserEntityWithWrongHashedPass);

            var result = await userService.Login(UserFixture.LoginUserDTOWithDifferentPassword);

            var resultType = Assert.IsType<Result<UserDTO>>(result);
            mockUserRepository.Verify(x => x.GetByEmail(UserFixture.LoginUserDTO.Email), Times.Once);
            Assert.False(resultType.IsSuccess);
            Assert.Null(resultType.Value);
            Assert.Equal("Password invalid", resultType.Message);
            Assert.Equal(400, resultType.Code);
        }
    }
}
