using System;
using System.Collections.Generic;

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
                var user = _userFactory.Create(userDto.Username, userDto.Email);
                if (_userFactory.Success)
                {
                    _database.Save(user);
                    _log($"User created {user.Username}");
                }
                else
                {
                    _log($"User could not be created");
                    foreach (var error in _userFactory.Errors)
                    {
                        _log(error);
                    }
                }
            }
        }
    }
}