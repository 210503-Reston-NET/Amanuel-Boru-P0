using StoreModels;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System;

namespace StoreDL
{
    public class LocationRepo
    {
        private const string filePath = "../StoreDL/Location.json";
        private string jsonString;

        public Location AddLocation(Location newLocation){

            List<Location> FileLocations = GetAllLocations();
            FileLocations.Add(newLocation);
            jsonString = JsonSerializer.Serialize(FileLocations);
            File.WriteAllText(filePath, jsonString);
            return newLocation;
        }

        public List<Location> GetAllLocations(){
            try
            {
                jsonString = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Location>();
            }
            return JsonSerializer.Deserialize<List<Location>>(jsonString);
        }

        public Location AddItemToLocation(Item newItem, Location newLocation){
            List<Location> FileLocations = GetAllLocations();
            int index = FindLocationIndex(newLocation);
            
            FileLocations.ElementAt(index).Inventory.Add(newItem);

            jsonString = JsonSerializer.Serialize(FileLocations);
            File.WriteAllText(filePath, jsonString);
            return newLocation;

        }

        public Location ReplenishItem(Location newLocation, Item newItem, int AddAmount){
            List<Location> FileLocations = GetAllLocations();
            
            
            int index = FindLocationIndex(newLocation);
            int itemIndex = FileLocations.ElementAt(index).GetItemIndex(newItem);
            FileLocations.ElementAt(index).Inventory.ElementAt(itemIndex).Quantity += AddAmount;
            
            jsonString = JsonSerializer.Serialize(FileLocations);
            File.WriteAllText(filePath, jsonString);
            return newLocation;
        }

        public int FindLocationIndex(Location newLocation){
            List<Location> FileLocations = GetAllLocations();
            int amount = FileLocations.Count;

            for (int i = 0; i < amount; i++){
                if (FileLocations.ElementAt(i).Equals(newLocation)) return i;
            }
            return -1;
        }
        
        public Location GetLocation(Location newLocation){
            return GetAllLocations().FirstOrDefault(Location => Location.Equals(newLocation));
        }
    }
}