using StoreModels;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System;

namespace StoreDL
{
    public class ProductRepo
    {
        private const string filePath = "../StoreDL/Product.json";
        private string jsonString;

        public Product AddProduct(Product newProduct){

            List<Product> FileLocations = GetAllProduct();
            FileLocations.Add(newProduct);
            jsonString = JsonSerializer.Serialize(FileLocations);
            File.WriteAllText(filePath, jsonString);
            return newProduct;
        }

        public List<Product> GetAllProduct(){
            try
            {
                jsonString = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Product>();
            }
            return JsonSerializer.Deserialize<List<Product>>(jsonString);
        }
        
        public Product GetProduct(Product newProduct){
            return GetAllProduct().FirstOrDefault(Product => Product.Equals(newProduct));
        }
    }
}