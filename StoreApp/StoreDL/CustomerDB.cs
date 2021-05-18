using System.Collections.Generic;
using Model = StoreModels;
using Entity = StoreDL.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StoreDL
{
    public class CustomerDB
    {
        private Entity.p0storeContext _context;
        public CustomerDB(Entity.p0storeContext context)
        {
            _context = context;
        }

        public Model.Customer AddCustomer(Model.Customer newCustomer){
            _context.Customers.Add(
                new Entity.Customer{
                    Username = newCustomer.UserName,
                    Name = newCustomer.Name
                }
            );

            _context.SaveChanges();
            return newCustomer;
        }
        public List<Model.Customer> GetAllCustomers(){
            return _context.Customers.Select( customer => new Model.Customer(customer.Name, customer.Username)).ToList();
        }

        public Model.Customer GetCustomer(string username){
            Entity.Customer found = _context.Customers.FirstOrDefault(custom =>  custom.Username == username);

            if (found == null) return null;
            else{
                return new Model.Customer(found.Name, found.Username);
            }
        }
    }
}