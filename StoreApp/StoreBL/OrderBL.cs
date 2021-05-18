using StoreDL;
using StoreModels;
using System.Collections.Generic;
using System;

namespace StoreBL
{
    public class OrderBL
    {
        private OrderRepo _repo;
        private OrderDB _orderDB;
        public OrderBL(OrderDB orderDB)
        {
            _orderDB = orderDB;
            _repo = new OrderRepo();
        }

        public Order AddOreder(Order newOrder, Location location){
            newOrder.calculateTotal();
            return _orderDB.AddOrder(newOrder, location);
        }

        public List<Order> GetAllOrder(){
            return _orderDB.GetAllOrder();
        }

        public Order GetOrder(Order newOrder){
            return _repo.GetOrder(newOrder);
        }

        public double OrderTotal(Order order){
            List<Item> items = order.Items;
            double total = 0;
            double price;

            foreach(Item item in items){
                price = item.Product.Price * item.Quantity;
                total += price;
            }

            return total;
        }

        public List<Order> CustomerOrdersBydate(Customer customer){
            return _orderDB.GetCustomerOrderByDate(customer);
        }

        public List<Order> CustomerOrdersByTotal(Customer customer){
            return _orderDB.GetCustomerOrderByTotal(customer);
        }
    }
}