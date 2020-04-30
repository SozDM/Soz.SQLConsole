using System;
using System.Collections.Generic;
using System.Text;

namespace Soz.SQLConsole
{
    public class User
    {
        public int Id { get; private set; }
        public string Name{ get; private set; }
        public string Address { get; private set; }
        
        public User()
        {
        }
        public virtual ICollection<Order> Orders { get; set; }

        public void ShowHelp()
        {
            Console.WriteLine("\t\"user-add\"\t\tto add user");
            Console.WriteLine("\t\"user-order-show\"\tto show all orders of this user");
            Console.WriteLine("\t\"order-add\"\t\tto add order");
            Console.WriteLine("\t\"order-add-rnd\"\t\tto add random orders");
            Console.WriteLine("\t\"order-edit\"\t\tto edit order");
            Console.WriteLine("\t\"order-del\"\t\tto delete order");
            Console.WriteLine("\t\"order-show\"\t\tto show all orders");
            Console.WriteLine("\t\"exit\"\t\t\tto exit program");
        }

        public void AddUser()       //create new user and add to database
        {
            string name = "", address = "";
            int Id = 0;

            name = name.InputStringNotWhiteSpace("name");

            address = address.InputStringNotWhiteSpace("address");
            
            using (var context = new MyDBContext())     //saving user to database
            {
                var user = new User
                {
                    Name = name,
                    Address = address
                };
                context.Users.Add(user);
                context.SaveChanges();
                Id = user.Id;
            }
            Console.WriteLine($"User #{Id} created");
        }
    }
}
