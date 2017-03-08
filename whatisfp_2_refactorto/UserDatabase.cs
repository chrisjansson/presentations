using System;

namespace Users
{

    public class FakeDatabase : IDatabase
    {
        public void Save(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
        }
    
        public void AssertUserSaved(string username) 
        {

        }
    }
}