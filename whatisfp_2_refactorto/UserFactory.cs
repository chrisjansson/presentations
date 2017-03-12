using System.Collections.Generic;
using Utils;

namespace Users
{
    public class UserFactory
    {
        private const int MinimumUsernameLength = 5;

        public Result<User, IEnumerable<string>> Create(string username, string email)
        {
            return Result
                .Validate((username, email), ValidateUsername, ValidateEmail)
                .Map(v => new User
                {
                    Username = v.Item1,
                    Email = v.Item2
                });
        }

        private static Maybe<string> ValidateEmail((string, string email) x)
        {
            return !x.email.Contains("@") ? "Email must contain a @".ToMaybe() : Maybe<string>.None;
        }

        private static Maybe<string> ValidateUsername((string username, string) x)
        {
            return x.username.Length < MinimumUsernameLength ? "Username must be atleast 5 characters".ToMaybe() : Maybe<string>.None;
        }
    }
}