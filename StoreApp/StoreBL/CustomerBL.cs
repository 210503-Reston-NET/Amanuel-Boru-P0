using System;
using StoreDL;
using StoreModels;

using System.Collections.Generic;

namespace StoreBL
{
    public class CustomerBL
    {
        private CustomerRepo _repo;

        public CustomerBL(CustomerRepo newRepo){
            _repo = newRepo;
        }
        public Customer AddCustomer(Customer newCustomer){
            return _repo.AddCustomer(newCustomer);
        }

        public bool UserNameExists(string userName){
            if (_repo.GetCustomer(userName) != null) return true;

            return false;
        }

        public List<Customer> GetAllCustomers(){
            return _repo.GetAllCustomer();
        }

        public Customer GetCustomer(string username){
            return _repo.GetCustomer(username);
        }
    }
}