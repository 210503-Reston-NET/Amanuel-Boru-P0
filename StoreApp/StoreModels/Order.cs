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
            Items = items;
        }
        public Customer Customer { get; set; }
        public Location Location { get; set; }
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
            string products = "";

            foreach(Item item in Items){
                products = string.Join("\n", item.ToString());
            }

            return $" customer: {Customer.ToString()} \t Total: {Total} \n {products}";
        }
    }
}