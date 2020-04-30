using System;
using System.Collections.Generic;
using System.Text;

namespace Soz.SQLConsole
{
    public class UserManager
    {
        public int Id { get; private set; }
        public string Name{ get; private set; }
        public string Address { get; private set; }
        
        public UserManager()
        {
        }
        public virtual ICollection<Order> Orders { get; set; }

        public string AddUser(string name, string address)       //create new user and add to database
        {
            int Id = 0;
            
            using (var context = new MyDBContext())     //saving user to database
            {
                var user = new UserManager
                {
                    Name = name,
                    Address = address
                };
                context.UserManagers.Add(user);
                context.SaveChanges();
                Id = user.Id;
            }
            return $"User #{Id} created";
        }
    }
}
