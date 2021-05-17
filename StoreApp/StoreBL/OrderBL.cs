using StoreDL;
using StoreModels;
using System.Collections.Generic;
using System;

namespace StoreBL
{
    public class OrderBL
    {
        OrderRepo _repo;
        public OrderBL(OrderRepo repo)
        {
            _repo = repo;
        }

        public Order AddOreder(Order newOrder){
            newOrder.calculateTotal();
            return _repo.AddOrder(newOrder);
        }

        public List<Order> GetAllOrder(){
            return _repo.GetAllOrder();
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

        public List<Order> CustomerOrders(Customer customer){
            List<Order> allOrders = GetAllOrder();
            List<Order> customerOrders = new List<Order>();

            foreach(Order order in allOrders){
                if (order.Customer.UserName.Equals(customer.UserName)){
                    customerOrders.Add(order);
                }
            }
            return customerOrders;
        }
    }
}