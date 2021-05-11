using System.Text.Json.Serialization;
namespace StoreModels
{    /// <summary>
    /// This data structure models a product and its quantity. The quantity was separated from the product as it could vary from orders and locations.  
    /// </summary>
    public class Item
    {
        public Item(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public bool Equals(Item newItem){
            return this.Product.Equals(newItem.Product);
        }
        public override string ToString()
        {
            return $" {Product.ToString()} \t Quantity: {Quantity}";
        }
    }
}