using System;
using StoreDL;
using StoreModels;
using System.Collections.Generic;

namespace StoreBL
{
    public class LocationBL
    {
        private LocationRepo _repo;
        private LocationDB _locationDB;

        public LocationBL(LocationDB newLocationRepo){
            _locationDB = newLocationRepo;
            _repo = new LocationRepo();
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
            Location existingLocation = _repo.GetLocation(newLocation);
            if (existingLocation == null){ 
                throw new Exception("Location does not exist");
            }
            else if (existingLocation.GetItem(newItem) != null){
                throw new Exception("Item already exists at location");
            }
            else{
                _repo.AddItemToLocation(newItem, existingLocation);
            }
        }

        public void ReplenishItem(Location newLocation, Item newItem, int AddAmount){
            if (_repo.GetLocation(newLocation) == null){
                throw new Exception("Location doesnt exist");
            }
            else if (_repo.GetLocation(newLocation).GetItem(newItem) == null){
                throw new Exception("Item doesnt exist at that location");
            }
            else{
                _repo.ReplenishItem(newLocation, newItem, AddAmount);
            }
        }

        public List<Item> GetInventory(Location location){
            if (LocationExists( location) == false){
                throw new Exception("Location does not exist");
            }
            return _locationDB.GetInventory(location);
        }
    }
}