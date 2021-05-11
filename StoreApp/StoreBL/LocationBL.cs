using System;
using StoreDL;
using StoreModels;
using System.Collections.Generic;

namespace StoreBL
{
    public class LocationBL
    {
        private LocationRepo _repo;

        public LocationBL(LocationRepo newLocationRepo){
            _repo = newLocationRepo;
        }

        public Location AddLocation(Location newLocation){
            return _repo.AddLocation(newLocation);
        }

        public List<Location> GetAllLocations(){
            return _repo.GetAllLocations();
        }

        public bool LocationExists(Location newLocation){
            if (_repo.GetLocation(newLocation) != null) return true;

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
    }
}