using StoreModels;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System;

namespace StoreDL
{
    public class ItemRepo
    {
        private const string filePath = "../StoreDL/Item.json";
        private string jsonString;

        public Item AddItem(Item newItem){

            List<Item> FileItem = GetAllItem();
            FileItem.Add(newItem);
            jsonString = JsonSerializer.Serialize(FileItem);
            File.WriteAllText(filePath, jsonString);
            return newItem;
        }

        public List<Item> GetAllItem(){
            try
            {
                jsonString = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Item>();
            }
            return JsonSerializer.Deserialize<List<Item>>(jsonString);
        }
        
        public Item GetItem(Item newItem){
            return GetAllItem().FirstOrDefault(Item => Item.Equals(newItem));
        }
    }
}