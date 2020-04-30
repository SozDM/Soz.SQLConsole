using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Soz.SQLConsole
{
    class Program
    {
        static void Main()
        {
            var x = ConnectToDatabaseAsync();

            Console.WriteLine("Input \"help\" for help");

            bool Stay = true;
            while (Stay)
            {

                var user = new User();
                var order = new Order();

                Console.Write("Input command: ");
                string command = Console.ReadLine();
                command.ToLower();

                switch (command)
                {
                    case "help":
                        user.ShowHelp();
                        break;

                    case "user-add":
                        user.AddUser();
                        break;

                    case "user-order-show":
                        order.ShowOrdersByUser();
                        break;

                    case "order-show":
                        order.ShowOrders();
                        break;

                    case "order-add":
                        order.AddOrder();
                        break;


                    case "order-edit":
                        order.EditOrder();
                        break;

                    case "order-del":
                        order.DeleteOrder();
                        break;

                    case "login":
                        break;//todo сделать возможность логина в базу


                    case "exit":
                        Stay = false;
                        break;

                }
            }
        }
        public static void ConnectToDatabase()
        {
            using (var context = new MyDBContext()) 
            {
            }
        }
        public static async Task ConnectToDatabaseAsync()
        {
            await Task.Run(() => ConnectToDatabase());
        }
    }
}
