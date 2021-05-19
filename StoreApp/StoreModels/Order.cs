using System;
using System.Collections.Generic;

namespace StoreModels
{    
    /// <summary>
    /// This class should contain all the fields and properties that define a customer order. 
    /// </summary>
    public class Order
    {
        public Order(Customer customer, Location location, List<Item> items)
        {
            Customer = customer;
            Location = location;
            Orderdate = DateTime.Now;
            Items = items;
        }

        public Order(Customer customer, Location location, DateTime orderdate , List<Item> items, double total)
        {
            Customer = customer;
            Location = location;
            Orderdate = orderdate;
            Items = items;
            Total = total;
        }

        public Order(){

        }
        public Customer Customer { get; set; }
        public Location Location { get; set; }
        public DateTime Orderdate { get; set;}
        public List<Item> Items { get; set; }
        public double Total { get; set; }

        //TODO: add a property for the order items

        public void calculateTotal(){
            double total = 0;
            double itemPrice;

            foreach(Item item in Items){
                itemPrice = item.Product.Price * item.Quantity;
                total += itemPrice;
            }

            Total = total;
        }

        public override string ToString()
        {
            return $" customer name: {Customer.UserName} \t\t Total: {Total} \t {Orderdate}";
        }
    }
}