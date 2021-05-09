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
        
        public Location GetLocation(Location newLocation){
            return GetAllLocations().FirstOrDefault(Location => Location.Equals(newLocation));
        }
    }
}