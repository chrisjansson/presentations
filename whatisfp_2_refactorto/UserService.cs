using System;
using System.Collections.Generic;
using Utils;

namespace Users
{
    public class UserService
    {
        private UserFactory _userFactory = new UserFactory();
        private IDatabase _database;
        private readonly Action<string> _log;

        public UserService(Action<string> log, IDatabase database)
        {
            _log = log;
            _database = database;
        }

        public void ImportUsers(IReadOnlyList<UserDto> users)
        {
            foreach (var userDto in users)
            {
                EnsureParameters(userDto)
                    .Bind(dto => _userFactory.Create(dto.Username, dto.Email))
                    .Execute(user => Save(user), errors => Log(errors));
            }
        }

        private Result<UserDto, IEnumerable<string>> EnsureParameters(UserDto parameters)
        {
            return Result.Validate(parameters, 
                x => x.Username == null ? $"{nameof(parameters.Username)} cannot be null".ToMaybe() : Maybe<string>.None,
                x => x.Email == null ? $"{nameof(parameters.Email)} cannot be null".ToMaybe() : Maybe<string>.None);
        }

        private void Save(User user)
        {
            _database.Save(user);
            _log($"User created {user.Username}");
        }

        private void Log(IEnumerable<string> errors)
        {
            _log("User could not be created");
            foreach(var error in errors)
                _log($"Error: {error}");
        }
    }
}