using System.Collections.Generic;

namespace Users
{
    public class UserFactory
    {
        private const int MinimumUsernameLength = 5;

        public UserFactory()
        {
            Success = false;
            Errors = new List<string>();
        }

        public User Create(string username, string email)
        {
            if (username.Length < MinimumUsernameLength)
            {
                Success = false;
                Errors.Add("Username must be atleast 5 characters");
                return null;
            }

            if (!email.Contains("@"))
            {
                Errors.Add("Email must contain a @");
                return null;
                Success = false;
            }

            return new User
            {
                Username = username,
                Email = email
            };
        }

        public bool Success { get; private set; }

        public List<string> Errors { get; private set; }
    }
}