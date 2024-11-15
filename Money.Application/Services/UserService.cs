using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Money.Application.Common;
using Money.Domain.Contracts.Repositories;
using Money.Domain.Contracts.Services;
using Money.Domain.DTOs.User;
using Money.Domain.Entities;
using Money.Domain.Helpers;

namespace Money.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper) 
        { 
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserDTO>> Create(CreateUserDTO input)
        {
            if (!EmailChecker.Check(TextCleaner.RemoveWhiteSpaces(input.Email)) || string.IsNullOrEmpty(input.Email))
                return new Result<UserDTO>().Failure("Email invalid", 400);

            var isEmailAlreadyInUse = await _userRepository.GetByEmail(TextCleaner.RemoveWhiteSpaces(input.Email)) != null;

            if (isEmailAlreadyInUse)
                return new Result<UserDTO>().Failure("Email already in use.", 400);

            if (string.IsNullOrEmpty(input.Name))
                return new Result<UserDTO>().Failure("Username invalid", 400);

            var isUserNameAlreadyInUse = await _userRepository.GetByUsername(input.Name) != null;

            if (isUserNameAlreadyInUse)
                return new Result<UserDTO>().Failure("Username already in use.", 400);

            if (TextCleaner.RemoveWhiteSpaces(input.ConfirmPassword) != TextCleaner.RemoveWhiteSpaces(input.Password))
                return new Result<UserDTO>().Failure("Password doesnt match", 400);

            if (!PasswordStrengthChecker.PasswordValidation(input.Password, out string errorMessafePassword))
                return new Result<UserDTO>().Failure(errorMessafePassword, 400);

            var userEntityMap = _mapper.Map<UserEntity>(input);
            if (userEntityMap == null)
                return new Result<UserDTO>().Failure("Something happened. Try again.", 500);

            var hasher = Hashing<UserEntity>.HashPassword(userEntityMap, userEntityMap.Password);

            userEntityMap.Password = hasher;

            userEntityMap.Indexes = CreateIndexes<UserEntity>.Create(userEntityMap);

            var userReturned = await _userRepository.Create(userEntityMap);

            if (!userReturned)
                return new Result<UserDTO>().Failure("Cannot create user. Try again later.", 500);

            var userDTO = _mapper.Map<UserDTO>(userReturned);

            return new Result<UserDTO>().Success("user created successfully", userDTO, 200);
        }


        public async Task<Result<UserDTO>> Login(LoginUserDTO input)
        {
            if (!EmailChecker.Check(TextCleaner.RemoveWhiteSpaces(input.Email)) || string.IsNullOrEmpty(input.Email))
                return new Result<UserDTO>().Failure("Email invalid", 400);

            if (!PasswordStrengthChecker.PasswordValidation(input.Password, out string message) || string.IsNullOrEmpty(input.Password))
                return new Result<UserDTO>().Failure("Password invalid", 400);

            var userFounded = await _userRepository.GetByEmail(input.Email);

            if (userFounded == null)
                return new Result<UserDTO>().Failure("User not found. Consider create a new account", 404);

            if (Hashing<UserEntity>.VerifyHashedPassword(userFounded, userFounded.Password, input.Password) == PasswordVerificationResult.Failed)
                return new Result<UserDTO>().Failure("Password invalid", 400);

            var userDTO = _mapper.Map<UserDTO>(userFounded);

            return new Result<UserDTO>().Success("User logged successfully.", userDTO, 200);
        }

        public Task<Result<bool>> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserDTO>> Read(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserDTO>> Update(UpdateUserDTO input)
        {
            throw new NotImplementedException();
        }
    }
}
