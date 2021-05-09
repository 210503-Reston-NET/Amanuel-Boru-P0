using System;
using StoreDL;
using StoreModels;

using System.Collections.Generic;

namespace StoreBL
{
    public class StoresBL
    {
        private StoreRepo _repo;

        public StoresBL(StoreRepo newRepo){
            _repo = newRepo;
        }
        public Customer AddCustomer(Customer newCustomer){
            return _repo.AddCustomer(newCustomer);
        }

        public bool UserNameExists(string userName){
            if (_repo.GetCustomer(userName) != null) return true;

            return false;
        }
    }
}