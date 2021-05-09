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
    }
}