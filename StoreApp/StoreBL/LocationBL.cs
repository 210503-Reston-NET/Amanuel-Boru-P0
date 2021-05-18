using System;
using StoreDL;
using StoreModels;
using System.Collections.Generic;

namespace StoreBL
{
    public class LocationBL
    {
        private LocationDB _locationDB;

        public LocationBL(LocationDB newLocationRepo){
            _locationDB = newLocationRepo;
        }

        public Location AddLocation(Location newLocation){
            if (LocationExists(newLocation)) {
                throw new Exception("Location already exists");
            }
            return _locationDB.AddLocation(newLocation);
        }

        public List<Location> GetAllLocations(){
            return _locationDB.GetAllLocations();
        }

        public bool LocationExists(Location newLocation){
            if (_locationDB.GetLocation(newLocation) != null) return true;

            return false;
        }
        
        public void AddItemToLocation(Location newLocation, Item newItem){
            Location existingLocation = _locationDB.GetLocation(newLocation);
            if (existingLocation == null){ 
                throw new Exception("Location does not exist");
            }
            else{
                _locationDB.AddItemToLocation(newLocation, newItem);
            }
        }

        public List<Item> GetInventory(Location location){
            if (LocationExists( location) == false){
                throw new Exception("Location does not exist");
            }
            return _locationDB.GetInventory(location);
        }

        public void changeInventory(Location location, Item item, int amount){
            _locationDB.changeInventory(location, item, amount);
        }
    }
}