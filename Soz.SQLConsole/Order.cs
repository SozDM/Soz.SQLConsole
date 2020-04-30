using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace Soz.SQLConsole
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public Order()
        {
        }

        public void ShowOrders()
        {
            using (var context = new MyDBContext())
            {
                var orders = context.Orders;
                foreach (var item in orders)
                {
                    item.ShowOrderInfo();
                }
            }
        }

        public void ShowOrdersByUser()
        {
            using (var context = new MyDBContext())
            {

                var UserIdList = new List<int>();
                string userId = "";
                
                var users = context.Users;
                foreach (var item in users)
                {
                    UserIdList.Add(item.Id);
                }

                userId = userId.InputIdByString("UserId", UserIdList);
                int intUserId = Int32.Parse(userId);

                var orders = context.Orders;

                int OrdersByThisUser = 0;

                foreach (var item in orders)
                {
                    if (item.UserId == intUserId)
                    {
                        OrdersByThisUser++;
                        item.ShowOrderInfo();
                    }
                }
                if (OrdersByThisUser == 0) Console.WriteLine("There are no orders by this user");
            }
        }

        public void AddOrder()
        {
            var UserIdList = new List<int>();

            using (var context = new MyDBContext())
            {
                var users = context.Users;
                foreach (var item in users)
                {
                    UserIdList.Add(item.Id);
                }
            }

            string amount = "", description = "", userId = "";

            userId = userId.InputIdByString("UserId", UserIdList);

            amount = amount.InputIntByString("amount");

            description = description.InputStringNotWhiteSpace("description");

            using (var context = new MyDBContext())     //saving user to database
            {
                var order = new Order
                {
                    Amount = Int32.Parse(amount),
                    Description = description,
                    Date = DateTime.Now,
                    UserId = Int32.Parse(userId)
                };

                context.Orders.Add(order);
                context.SaveChanges();
                Id = order.Id;
            }
            Console.WriteLine($"Order #{Id} from user#{userId} created");
        }

        public void EditOrder()
        {
            using (var context = new MyDBContext())
            {
                var orders = context.Orders;
                var OrderIdList = new List<int>();

                foreach (var item in orders)
                {
                    OrderIdList.Add(item.Id);
                }
                string OrderToChangeId = "";
                OrderToChangeId = OrderToChangeId.InputIdByString("OrderId", OrderIdList);
                int IntOrderToEditId = Int32.Parse(OrderToChangeId);

                var OrderToEdit = context.Orders.First(order => order.Id == IntOrderToEditId);
                OrderToEdit.ShowOrderInfo();

                string NewAmount = "";
                NewAmount = NewAmount.InputIntByString("amount");
                int IntNewAmount = Int32.Parse(NewAmount);

                string NewDescription = "";
                NewDescription = NewDescription.InputStringNotWhiteSpace("description");

                OrderToEdit.Amount = IntNewAmount;
                OrderToEdit.Description = NewDescription;
                context.SaveChanges();
            }
        }

        public void DeleteOrder()
        {
            using (var context = new MyDBContext())
            {
                var orders = context.Orders;
                var OrderIdList = new List<int>();

                foreach (var item in orders)
                {
                    OrderIdList.Add(item.Id);
                }

                string OrderToDeleteId = "";
                OrderToDeleteId = OrderToDeleteId.InputIdByString("OrderId", OrderIdList);

                int IntOrderToDeleteId = Int32.Parse(OrderToDeleteId);
                var OrderToDelete = context.Orders.First(order => order.Id == IntOrderToDeleteId);
                context.Orders.Remove(OrderToDelete);
                context.SaveChanges();

                Console.WriteLine($"Order #{OrderToDeleteId} is deleted");
            }
        }
    }
}