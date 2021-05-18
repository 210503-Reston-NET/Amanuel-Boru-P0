using System.Collections.Generic;
using Model = StoreModels;
using Entity = StoreDL.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace StoreDL
{
    public class OrderDB
    {
        private Entity.p0storeContext _context;
        private CustomerDB customerDB;

        private LocationDB locationDB;
        public OrderDB(Entity.p0storeContext context)
        {
            _context = context;
            customerDB = new CustomerDB(_context);
            locationDB = new LocationDB(_context);
            
        }

        
        public List<Model.Order> GetAllOrder(){
            
            return _context.Orders
            .Select( order => new Model.Order(
                new Model.Customer(order.Cusername), 
                new Model.Location((int)order.LocationId), 
                (DateTime)order.Orderdate, 
                null,
                (double)order.Total
                ))
                .ToList();
        }

        public Model.Order AddOrder(Model.Order newOrder, Model.Location location){

            Entity.Order DBOrder  = new Entity.Order{
                Cusername = newOrder.Customer.UserName,
                LocationId = location.LocationID,
                Orderdate = newOrder.Orderdate,
                Total = newOrder.Total
            };
            _context.Orders.Add(DBOrder);
            _context.SaveChanges();
            
            List<Model.Item> items = newOrder.Items;

            foreach(Model.Item item in items){
                _context.Items.Add(
                    new Entity.Item {
                        LocationId = DBOrder.LocationId,
                        OrderId = DBOrder.OrderId,
                        ProductName = item.Product.ProductName,
                        Price = item.Product.Price,
                        Quantity = item.Quantity
                    }
                );
                changeInventory(location, item, -1*item.Quantity);
            }
            _context.SaveChanges();
            return newOrder;
        }

        public List<Model.Item> GetItems(int orderID){
            List<Model.Item> items = _context.Items.Where(
                item => item.OrderId == orderID)
                .Select(
                    item => new Model.Item(new Model.Product(item.ProductName, (double)item.Price), (int)item.Quantity)
                ).ToList();

            return items;
        }

        public void changeInventory(Model.Location location, Model.Item item, int amount){
            
                Entity.Item Eitem = _context.Items.
                FirstOrDefault( itm => itm.OrderId == null && itm.LocationId == location.LocationID && itm.ProductName == item.Product.ProductName);

                Eitem.Quantity += amount;

                _context.SaveChanges();
        }
        public Model.Customer GetCustomer(string username){
            Model.Customer found = customerDB.GetCustomer(username);

            return new Model.Customer(found.Name, found.UserName);
        }

        public string GetCustomerName(string username){
            Model.Customer found = customerDB.GetCustomer(username);
            return found.Name;
        }

        public Model.Location GetLocation(int LocationID){
            Entity.Location found =  _context.Locations.FirstOrDefault(location => location.LocationId == LocationID);
            if (found == null) return null;
            return new Model.Location(found.LocationName, found.LocationAddress);
        }

        public List<Model.Order> GetCustomerOrderByDate(Model.Customer customer){
            List<Model.Order> allOrders = GetAllOrder();
            List<Model.Order> customerOrder = new List<Model.Order>();

            foreach(Model.Order order in allOrders){
                if (order.Customer.UserName == customer.UserName){
                    customerOrder.Add(order);
                }
            }

            customerOrder.Sort(delegate(Model.Order x, Model.Order y)
                {
                    return x.Orderdate.CompareTo(y.Orderdate);
                });

            return customerOrder;
        }

        public List<Model.Order> GetCustomerOrderByTotal(Model.Customer customer){
            List<Model.Order> allOrders = GetAllOrder();
            List<Model.Order> customerOrder = new List<Model.Order>();

            foreach(Model.Order order in allOrders){
                if (order.Customer.UserName == customer.UserName){
                    customerOrder.Add(order);
                }
            }

            customerOrder.Sort(delegate(Model.Order x, Model.Order y)
                {
                    return x.Total.CompareTo(y.Total);
                });

            return customerOrder;
        }
    }
}