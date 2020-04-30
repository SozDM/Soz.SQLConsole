using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Soz.SQLConsole
{
    class Program
    {

        static void Main()
        {
            //var x = ConnectToDatabaseAsync();
            Console.WriteLine("Connecting to database...");

            bool Stay = true, PrintHelp = true;
            while (Stay)
            {
                var userManager = new UserManager();
                var order = new Order();
                string HowMany = "";
                int IntHowMany = 0;
                var UserIdList = new List<int>();
                var OrderIdList = new List<int>();

                using (var context = new MyDBContext())
                {
                    var users = context.Users;
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
                        userManager.ShowHelp();
                        break;

                    case "user-add":
                        string UserName = "";
                        UserName = UserName.InputStringNotWhiteSpace("name");
                        string UserAddress = "";
                        UserAddress = UserAddress.InputStringNotWhiteSpace("address");
                        Console.WriteLine(userManager.AddUser(UserName, UserAddress));
                        break;

                    case "user-order-show":
                        string strUserId = "";
                        strUserId = strUserId.InputIdByString("UserId", UserIdList);
                        var OrderList = order.OrdersIdByUser(Int32.Parse(strUserId));
                        if (OrderList[0] == 0) Console.WriteLine("There is no orders by this user");
                        else
                        {
                            foreach (var item in OrderList)
                            {
                                Console.Write(order.OrderInfo(item));
                            }
                        }
                        break;

                    case "order-show-all":
                        foreach(var item in OrderIdList) 
                        { 
                            Console.Write(order.OrderInfo(item));
                        }
                        break;

                    case "order-add":
                        strUserId = "";
                        strUserId = strUserId.InputIdByString("UserId", UserIdList);
                        if (strUserId == "exit") break;

                        string amount = "";
                        amount = amount.InputIntByString("amount");
                        if (amount == "exit") break;

                        string description = "";
                        description = description.InputStringNotWhiteSpace("description");
                        if (description == "exit") break;

                        var orderId = order.AddOrder(Int32.Parse(strUserId),Int32.Parse(amount), description);
                        Console.WriteLine($"Order #{orderId} successfully added");
                        
                        break;

                    case "order-add-rnd":
                        HowMany = HowMany.InputIntByString("number of orders");
                        if (HowMany == "exit") break;
                        IntHowMany = Int32.Parse(HowMany);
                        order.AddRandomOrders(IntHowMany, UserIdList);
                        break;

                    case "order-edit":
                        string strOrderId = "";
                        strOrderId = strOrderId.InputIntByString("orderId");
                        if (strOrderId == "exit") break;

                        amount = "";
                        if (amount == "exit") break;
                        amount = amount.InputIntByString("amount");
                        
                        description = "";
                        description = description.InputStringNotWhiteSpace("description");
                        if (description== "exit") break;

                        order.EditOrder(Int32.Parse(strOrderId),Int32.Parse(amount), description);
                        Console.WriteLine($"Order #{Int32.Parse(strOrderId)} is edited");
                        break;

                    case "order-del":
                        strOrderId = "";
                        strOrderId = strOrderId.InputIdByString("OrderId", OrderIdList);
                        if (strOrderId == "exit") break;
                        order.DeleteOrder(Int32.Parse(strOrderId));
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
