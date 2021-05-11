using StoreModels;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System;

namespace StoreDL
{
    public class OrderRepo
    {
        private const string filePath = "../StoreDL/Order.json";
        private string jsonString;

        public Order AddOrder(Order newOrder){

            List<Order> FileLocations = GetAllOrder();
            FileLocations.Add(newOrder);
            jsonString = JsonSerializer.Serialize(FileLocations);
            File.WriteAllText(filePath, jsonString);
            return newOrder;
        }

        public List<Order> GetAllOrder(){
            try
            {
                jsonString = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Order>();
            }
            return JsonSerializer.Deserialize<List<Order>>(jsonString);
        }
        
        public Order GetOrder(Order newOrder){
            return GetAllOrder().FirstOrDefault(Order => Order.Equals(newOrder));
        }
    }
}