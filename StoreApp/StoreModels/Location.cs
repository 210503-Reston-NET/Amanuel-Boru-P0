using System.Collections.Generic;

namespace StoreModels
{
    /// <summary>
    /// This class should contain all the fields and properties that define a store location.
    /// </summary>
    public class Location
    {
        public Location(string locationName , string address){
            Address = address;
            LocationName = locationName;
            Inventory = new List<Item>();
        }
        public string Address { get; set; }
        public string LocationName { get; set; }

        public List<Item> Inventory { get; set;}
        //TODO: add some property for the location inventory

        public bool Equals(Location newLocation){
            return this.Address.Equals(newLocation.Address) && this.LocationName.Equals(newLocation.LocationName);
        }

        public Item GetItem(Item newItem){
            foreach(Item item in Inventory){
                if (item.Equals(newItem)) return item;
            }

            return null;
        }

        public int GetItemIndex(Item newItem){
            for(int i =0; i < Inventory.Count; i++){
                if (Inventory[i].Equals(newItem)){
                    return i;
                }
            }
            return -1;
        }

        public override string ToString()
        {
            return $" Location name: {LocationName} \n\t Address: {Address}";
        }
    }
}