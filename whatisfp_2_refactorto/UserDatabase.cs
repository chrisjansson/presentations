using System;

namespace Users
{
    public class UserDatabase 
    {
        public void Save(User user) 
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user));
        }
    }
}