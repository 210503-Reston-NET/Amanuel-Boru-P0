using StoreModels;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System;

namespace StoreDL
{
    public class CustomerRepo
    {
        private const string filePath = "../StoreDL/Customer.json";
        private string jsonString;

        public Customer AddCustomer(Customer newCustomer){

            List<Customer> FileCustomers = GetAllCustomer();
            FileCustomers.Add(newCustomer);
            jsonString = JsonSerializer.Serialize(FileCustomers);
            File.WriteAllText(filePath, jsonString);
            return newCustomer;
        }

        public List<Customer> GetAllCustomer(){
            try
            {
                jsonString = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Customer>();
            }
            return JsonSerializer.Deserialize<List<Customer>>(jsonString);
        }

        public Customer GetCustomer(string userName){
            return GetAllCustomer().FirstOrDefault(customer => customer.Equals(userName));
        }

    }
}