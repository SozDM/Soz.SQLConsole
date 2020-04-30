using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Soz.SQLConsole
{
    class Program
    {
        static UserManager userManager = new UserManager();
        static Order order = new Order();
        static string HowMany = "", strUserId = "", amount = "", description = "",
                      strOrderId = "";

        static int IntHowMany = 0;
        static List<int> UserIdList = new List<int>();
        static List<int> OrderIdList = new List<int>();

        static void Main()
        {
            //var x = ConnectToDatabaseAsync();
            Console.WriteLine("Connecting to database...");



            bool Stay = true, PrintHelp = true;
            while (Stay)
            {
                using (var context = new MyDBContext())
                {
                    var users = context.UserManagers;
                    foreach (var item in users)
                    {
                        UserIdList.Add(item.Id);
                    }
                    var orders = context.Orders;
                    foreach (var item in orders)
                    {
                        OrderIdList.Add(item.Id);
                    }
                }

                if (PrintHelp)
                {
                    Console.WriteLine("Input \"help\" for help");
                    PrintHelp = false;
                }

                Console.Write("Input command: ");
                string command = Console.ReadLine();
                command.ToLower();

                switch (command)
                {
                    case "help":
                        ShowHelp();
                        break;

                    case "user-add":
                        UserAdd();
                        break;

                    case "user-order-show":
                        ShowOrdersByUser();
                        break;

                    case "order-show-all":
                        OrdersShowAll();
                        break;

                    case "order-add":
                        OrderAdd();
                        break;

                    case "order-add-rnd":
                        OrdersAddRandom();
                        break;

                    case "order-edit":
                        OrderEdit();
                        break;

                    case "order-del":
                        OrderDelete();
                        break;

                    case "login":
                        break;//todo сделать возможность логина в базу

                    case "exit":
                        Stay = false;
                        break;
                }
            }

            void UserAdd()
            {
                string UserName = "";
                UserName = UserName.InputStringNotWhiteSpace("name");
                string UserAddress = "";
                UserAddress = UserAddress.InputStringNotWhiteSpace("address");
                Console.WriteLine(userManager.AddUser(UserName, UserAddress));
            }

        }

        static void ShowOrdersByUser()
        {
            strUserId = strUserId.InputIdByString("UserId", UserIdList);
            var OrderList = order.IdByUser(Int32.Parse(strUserId));
            if (OrderList[0] == 0) Console.WriteLine("There is no orders by this user");
            else
            {
                foreach (var item in OrderList)
                {
                    Console.Write(order.Info(item));
                }
            }
        }

        static void OrdersShowAll()
        {
            foreach (var item in OrderIdList)
            {
                Console.Write(order.Info(item));
            }
        }

        static void OrderAdd()
        {
            strUserId = strUserId.InputIdByString("UserId", UserIdList);
            if (strUserId == "exit") return;

            amount = amount.InputIntByString("amount");
            if (amount == "exit") return;

            description = description.InputStringNotWhiteSpace("description");
            if (description == "exit") return;

            var orderId = order.Add(Int32.Parse(strUserId), Int32.Parse(amount), description);
            Console.WriteLine($"Order #{orderId} successfully added");
        }

        static void OrdersAddRandom()
        {
            HowMany = HowMany.InputIntByString("number of orders");
            if (HowMany == "exit") return;
            IntHowMany = Int32.Parse(HowMany);
            order.AddRandom(IntHowMany, UserIdList);
        }

        static void OrderEdit()
        {
            strOrderId = strOrderId.InputIntByString("orderId");
            if (strOrderId == "exit") return;

            amount = "";
            if (amount == "exit") return;
            amount = amount.InputIntByString("amount");

            description = "";
            description = description.InputStringNotWhiteSpace("description");
            if (description == "exit") return;

            order.Edit(Int32.Parse(strOrderId), Int32.Parse(amount), description);
            Console.WriteLine($"Order #{Int32.Parse(strOrderId)} is edited");
        }

        static void OrderDelete()
        {
            strOrderId = strOrderId.InputIdByString("OrderId", OrderIdList);
            if (strOrderId == "exit") return;
            order.Delete(Int32.Parse(strOrderId));
        }

        static void ShowHelp()
        {
            Console.WriteLine("\t\"user-add\"\t\tto add user");
            Console.WriteLine("\t\"user-order-show\"\tto show all orders of this user");
            Console.WriteLine("\t\"order-add\"\t\tto add order");
            Console.WriteLine("\t\"order-add-rnd\"\t\tto add random orders");
            Console.WriteLine("\t\"order-show-all\"\t\tto show all orders");
            Console.WriteLine("\t\"order-edit\"\t\tto edit order");
            Console.WriteLine("\t\"order-del\"\t\tto delete order");
            Console.WriteLine("\t\"exit\"\t\t\tto exit program");
        }
    }
}
