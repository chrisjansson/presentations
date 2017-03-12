using Users;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class UserServiceTests
    {
        private UserService _userService;
        private FakeDatabase _fakeDatabase;

        public UserServiceTests(ITestOutputHelper output)
        {
            _fakeDatabase = new FakeDatabase();
            _userService = new UserService(l => output.WriteLine(l), _fakeDatabase);
        }

        [Fact]
        public void Imports_user()
        {
            ImportUsers(User("username", "some@email.com"));

            _fakeDatabase.AssertUserSaved("username");
        }


        [Theory]
        [InlineData("user")]
        [InlineData(null)]
        public void Does_not_import_user_with_invalid_username(string username)
        {
            ImportUsers(User(username, "some@email.com"));

            _fakeDatabase.AssertUserNotSaved(username);
        }

        [Theory]
        [InlineData("email")]
        public void Does_not_import_user_with_invalid_email(string email)
        {
            ImportUsers(User("username", email));

            _fakeDatabase.AssertUserNotSaved("username");
        }

        [Fact]
        public void Imports_multiple_users()
        {
            ImportUsers(
                User("username", "some@email.com"),
                User("username2", "some@email2.com"));

            _fakeDatabase.AssertUserSaved("username");
            _fakeDatabase.AssertUserSaved("username2");
        }

        [Fact]
        public void Imports_first_user_when_second_fails()
        {
            ImportUsers(
                User("username", "some@email.com"),
                User("user", "some@email.com"));

            _fakeDatabase.AssertUserSaved("username");
            _fakeDatabase.AssertUserNotSaved("user");
        }

        [Fact]
        public void Imports_second_user_when_first_fails()
        {
            ImportUsers(
                User("user", "some@email.com"),
                User("username", "some@email.com"));

            _fakeDatabase.AssertUserSaved("username");
            _fakeDatabase.AssertUserNotSaved("user");
        }

        private UserDto User(string username, string email)
        {
            return new UserDto
            {
                Username = username,
                Email = email
            };
        }

        private void ImportUsers(params UserDto[] users)
        {
            _userService.ImportUsers(users);
        }
    }
}