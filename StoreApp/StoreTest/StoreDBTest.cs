using Entity = StoreDL.Entities;
using Model = StoreModels;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using StoreDL;
using System.Linq;
using System;
namespace StoreTest
{
    public class StoreDBTest
    {
        private readonly DbContextOptions<Entity.p0storeContext> options;

        public StoreDBTest()
        {
            options = new DbContextOptionsBuilder<Entity.p0storeContext>().UseSqlite("Filename=Test.db").Options;
            seed();
        }

        [Fact]
        public void GetAllCustomersShouldReturnAllCustomers(){
            using (var context = new Entity.p0storeContext(options)){
                CustomerDB _customerDB = new CustomerDB(context);
                var customers = _customerDB.GetAllCustomers();
                int count = customers.Count;

                Assert.Equal(1, count);
            }
        }

        [Fact]
        public void GetCustomerReturnsCustomer(){
            using (var context = new Entity.p0storeContext(options)){
                CustomerDB _customerDB = new CustomerDB(context);
                var customer1 = _customerDB.GetCustomer("amanboru");
                string username = customer1.UserName;

                Assert.Equal("amanboru", username);
            }
        }

        [Fact]
        public void GetCustomerReturnsNull(){
            using (var context = new Entity.p0storeContext(options)){
                CustomerDB _customerDB = new CustomerDB(context);
                var customer1 = _customerDB.GetCustomer("doesntExist");

                Assert.Null(customer1);
            }
        }

        [Fact]
        public void AddCustomerWorks(){
            using (var context = new Entity.p0storeContext(options)){
                CustomerDB _customerDB = new CustomerDB(context);
                _customerDB.AddCustomer(new Model.Customer("john doe", "johndoe"));
            }

            using (var assertContext = new Entity.p0storeContext(options)){
                var customer = assertContext.Customers.FirstOrDefault(cust => cust.Username == "johndoe");
                Assert.NotNull(customer);
                Assert.Equal("john doe", customer.Name);
            }
        }

        [Fact]
        public void GetAllLocationsShouldReturnAllLocation(){
            using (var context = new Entity.p0storeContext(options)){
                LocationDB _locationDB = new LocationDB(context);
                var customers = _locationDB.GetAllLocations();
                int count = customers.Count;

                Assert.Equal(1, count);
            }
        }

        [Fact]
        public void GetLocationReturnsLocation(){
            using (var context = new Entity.p0storeContext(options)){
                LocationDB _locationDB = new LocationDB(context);
                var location = _locationDB.GetLocation(new Model.Location("cool store", "123 cool st Baltimore MD"));
                int locationID = location.LocationID;

                Assert.Equal(1, locationID);
            }
        }

        [Fact]
        public void GetLocationReturnsNull(){
            using (var context = new Entity.p0storeContext(options)){
                LocationDB _locationDB = new LocationDB(context);
                var location = _locationDB.GetLocation(new Model.Location("non existing store", "123 doesnt exist"));

                Assert.Null(location);
            }
        }

        [Fact]
        public void AddLocationWorks(){
            using (var context = new Entity.p0storeContext(options)){
                LocationDB _locationDB = new LocationDB(context);
                _locationDB.AddLocation(new Model.Location("new location", "123 new location"));
            }

            using (var assertContext = new Entity.p0storeContext(options)){
                var location = assertContext.Locations.FirstOrDefault(loca => loca.LocationName == "new location" && loca.LocationAddress == "123 new location");
                Assert.NotNull(location);
            }
        }

        [Fact]
        public void GetAllOrdersShouldReturnAllOrders(){
            using (var context = new Entity.p0storeContext(options)){
                OrderDB _orderDB = new OrderDB(context);
                var orders = _orderDB.GetAllOrder();
                int count = orders.Count;

                Assert.Equal(1, count);
            }
        }


        private void seed(){

            using (var context = new Entity.p0storeContext(options)){
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Customers.AddRange(
                    new Entity.Customer{
                        Username = "amanboru",
                        Name = "Amanuel Boru",
                        Orders = new List<Entity.Order>{
                            new Entity.Order {
                                OrderId = 1,
                                Cusername = "amanboru",
                                LocationId = 1,
                                Orderdate = DateTime.Now,
                                Total = 20.00,
                                Items = new List<Entity.Item> {
                                    new Entity.Item {
                                        ItemId = 1,
                                        LocationId = 1,
                                        OrderId = 1,
                                        ProductName = "Lily",
                                        Price = 5.00,
                                        Quantity = 4
                                    }
                                }
                            }
                        }
                    }
                );

                context.Locations.AddRange(
                    new Entity.Location {
                        LocationId = 1,
                        LocationName = "cool store",
                        LocationAddress = "123 cool st Baltimore MD",
                        Items = new List<Entity.Item> {
                            new Entity.Item {
                                ItemId = 2,
                                LocationId = 1,
                                OrderId = null,
                                ProductName = "Lily",
                                Price = 5.00,
                                Quantity = 15
                            },
                            new Entity.Item {
                                ItemId = 3,
                                LocationId = 1,
                                OrderId = null,
                                ProductName = "Rose",
                                Price = 9.99,
                                Quantity = 20
                            }
                        },
                    }
                );
                context.SaveChanges();
            }
        }
    }
}