using System.Collections.Generic;

namespace Users
{
    public class UserCreator
    {
        public User Create(string username, string email)
        {
            return new User
            {
                Username = username,
                Email = email
            };
        }

        public bool Success { get; set; }

        public List<string> Errors { get; set; }
    }
}