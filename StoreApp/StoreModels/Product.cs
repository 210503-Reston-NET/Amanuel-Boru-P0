namespace StoreModels
{
    //This class should contain all necessary fields to define a product.
    public class Product
    {
        public Product(string productName, double price)
        {
            ProductName = productName;
            Price = price;
        }
        public string ProductName { get; set; }
        public double Price { get; set; }
        //todo: add more properties to define a product (maybe a category?)

        public bool Equals(Product newProduct){
            return this.ProductName.Equals(newProduct.ProductName) && this.Price.Equals(newProduct.Price);
        }

        public override string ToString()
        {
            return $" Product name: {ProductName} \t Price: {Price}";
        }
    }
}