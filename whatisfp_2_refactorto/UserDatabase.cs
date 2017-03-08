using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Users
{
    public class FakeDatabase : IDatabase
    {
        private List<User> _users = new List<User>();

        public void Save(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _users.Add(user);
        }

        public void AssertUserSaved(string username)
        {
            Assert.Contains(username, _users.Select(x => x.Username));
        }

        public void AssertUserNotSaved(string username)
        {
            Assert.DoesNotContain(username, _users.Select(x => x.Username));
        }
    }
}