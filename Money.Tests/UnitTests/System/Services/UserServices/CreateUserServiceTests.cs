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
    public class CreateUserServiceTests
    {
        [Fact]
        public async Task CreateUserWithSuccessReturn200Ok()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            mockMapper
                .Setup(x => x.Map<UserEntity>(UserFixture.CreateUserDTO))
                .Returns(UserFixture.UserEntity);

            mockUserRepository
                 .Setup(x => x.Create(UserFixture.UserEntity))
                 .ReturnsAsync(true);

            var userService = new UserService(mockUserRepository.Object, mockMapper.Object);

            var result = await userService.Create(UserFixture.CreateUserDTO);

            var resultUser = Assert.IsType<Result<UserDTO>>(result);

            mockUserRepository
                .Verify(x => x.Create(UserFixture.UserEntity), Times.Once);

            mockMapper.Verify(x => x.Map<UserEntity>(UserFixture.CreateUserDTO), Times.Once);
            Assert.True(resultUser.IsSuccess);
            Assert.Equal("user created successfully", resultUser.Message);
        }

        [Fact]
        public async Task CreateUserIfEmailInvalidReturn400BadRequest()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userService = new UserService(mockUserRepository.Object, mockMapper.Object);

            var result = await userService.Create(UserFixture.CreateUserDTOWithInvalidEmail);
            var resultUser = Assert.IsType<Result<UserDTO>>(result);

            Assert.Null(resultUser.Value);
            Assert.False(resultUser.IsSuccess);
            Assert.Equal(400, resultUser.Code);
            Assert.Equal("Email invalid", resultUser.Message);
        }


        [Fact]
        public async Task CreateUserIfUsernameInvalidReturn400BadRequest()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userService = new UserService(mockUserRepository.Object, mockMapper.Object);

            var result = await userService.Create(UserFixture.CreateUserDTOWithInvalidUsername);
            var resultUser = Assert.IsType<Result<UserDTO>>(result);

            Assert.Null(resultUser.Value);
            Assert.False(resultUser.IsSuccess);
            Assert.Equal(400, resultUser.Code);
            Assert.Equal("Username invalid", resultUser.Message);
        }

        [Fact]
        public async Task CreateUserIfPasswordsDoesntMatchReturn400BadRequest()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userService = new UserService(mockUserRepository.Object, mockMapper.Object);

            var result = await userService.Create(UserFixture.CreateUserDTOWithPasswordDoesntMatching);
            var resultUser = Assert.IsType<Result<UserDTO>>(result);

            Assert.Null(resultUser.Value);
            Assert.False(resultUser.IsSuccess);
            Assert.Equal(400, resultUser.Code);
            Assert.Equal("Password doesnt match", resultUser.Message);
        }

        [Fact]
        public async Task CreateUserIfPasswordIsInvalidReturn400BadRequest()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userService = new UserService(mockUserRepository.Object, mockMapper.Object);

            var result = await userService.Create(UserFixture.CreateUserDTOWithPasswordIsInvalid);
            var resultUser = Assert.IsType<Result<UserDTO>>(result);

            Assert.Null(resultUser.Value);
            Assert.False(resultUser.IsSuccess);
            Assert.Equal(400, resultUser.Code);
        }

        [Fact]
        public async Task CreateUserIfUsernameAlreadyInUseReturns400BadRequest()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userService = new UserService(mockUserRepository.Object, mockMapper.Object);

            mockUserRepository
                .Setup(x => x.GetByUsername(UserFixture.CreateUserDTO.Name))
                .ReturnsAsync(UserFixture.UserEntity);

            var result = await userService.Create(UserFixture.CreateUserDTO);

            mockUserRepository.Verify(x => x.GetByUsername(UserFixture.CreateUserDTO.Name), Times.Once);
            var resultUser = Assert.IsType<Result<UserDTO>>(result);
            Assert.Null(resultUser.Value);
            Assert.False(resultUser.IsSuccess);
            Assert.Equal(400, resultUser.Code);
            Assert.Equal("Username already in use.", resultUser.Message);
        }

        [Fact]
        public async Task CreateUserIfEmailAlreadyInUseReturns400BadRequest()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userService = new UserService(mockUserRepository.Object, mockMapper.Object);

            mockUserRepository
                .Setup(x => x.GetByEmail(TextCleaner.RemoveWhiteSpaces(UserFixture.CreateUserDTO.Email)))
                .ReturnsAsync(UserFixture.UserEntity);

            var result = await userService.Create(UserFixture.CreateUserDTO);

            mockUserRepository.Verify(x => x.GetByEmail(TextCleaner.RemoveWhiteSpaces(UserFixture.CreateUserDTO.Email)), Times.Once);
            var resultUser = Assert.IsType<Result<UserDTO>>(result);
            Assert.Null(resultUser.Value);
            Assert.False(resultUser.IsSuccess);
            Assert.Equal(400, resultUser.Code);
            Assert.Equal("Email already in use.", resultUser.Message);
        }

        [Fact]
        public async Task CreateUserMapperErrorReturns500Exception()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userService = new UserService(mockUserRepository.Object, mockMapper.Object);

            var result = await userService.Create(UserFixture.CreateUserDTO);
            mockUserRepository
                .Setup(x => x.Create(UserFixture.UserEntity))
                .ReturnsAsync(false);

            var resultUser = Assert.IsType<Result<UserDTO>>(result);
            Assert.False(resultUser.IsSuccess);
            Assert.Equal("Something happened. Try again.", resultUser.Message);
            Assert.Equal(500, resultUser.Code);
        }

        [Fact]
        public async Task CreateUserIfCannotCreateUserReturns500Exception()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            mockMapper
                .Setup(x => x.Map<UserEntity>(UserFixture.CreateUserDTO))
                .Returns(UserFixture.UserEntity);

            var userService = new UserService(mockUserRepository.Object, mockMapper.Object);

            var result = await userService.Create(UserFixture.CreateUserDTO);
            mockUserRepository
                .Setup(x => x.Create(UserFixture.UserEntity))
                .ReturnsAsync(false);

            var resultUser = Assert.IsType<Result<UserDTO>>(result);

            mockUserRepository
                .Verify(x => x.Create(UserFixture.UserEntity), Times.Once);

            mockMapper
                .Verify(x => x.Map<UserEntity>(UserFixture.CreateUserDTO), Times.Once);

            Assert.False(resultUser.IsSuccess);
            Assert.Equal("Cannot create user. Try again later.", resultUser.Message);
            Assert.Equal(500, resultUser.Code);
        }
    }
}
