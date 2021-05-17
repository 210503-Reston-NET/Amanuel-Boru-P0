using System;
using System.Collections.Generic;
using StoreBL;
using StoreModels;
using StoreDL;

namespace StoreUI
{
    public class ManagerMenu
    {
        private LocationBL _locationBL;
        private CustomerBL _customerBL;
        private OrderBL _orderBL;
        
        public ManagerMenu(LocationBL locationbl, CustomerBL customerBL, OrderBL orderBL){
            _locationBL = locationbl;
            _customerBL = customerBL;
            _orderBL = orderBL;
        }
        public void start(){
            bool repeat = true;
            do{
                Console.WriteLine("\n\nPlease enter \"1\" to add a new location");
                Console.WriteLine("Please enter \"2\" to view all location location");
                Console.WriteLine("Please enter \"3\" to view location inventory");
                Console.WriteLine("Please enter \"4\" to add a new product");
                Console.WriteLine("Please enter \"5\" to replenish an item");
                Console.WriteLine("Please enter \"6\" to View all customers");
                Console.WriteLine("Please enter \"7\" to search a customer");
                Console.WriteLine("Please enter \"8\" to view all order");
                Console.WriteLine("Please enter \"9\" Go Back");
                string Response = Console.ReadLine();

                switch(Response){
                    case "1":
                        System.Console.WriteLine("add location");
                        AddLocation();
                        break;
                    case "2":
                        System.Console.WriteLine("show all locations");
                        ViewAllLocations();
                        break;
                    case "3":
                        System.Console.WriteLine("show location inventory");
                        ViewLocationInventory();
                        break;
                    case "4":
                        System.Console.WriteLine("add product");
                        AddProduct();
                        break;
                    case "5":
                        System.Console.WriteLine("replenish item");
                        ReplenishItem();
                        break;
                    case "6":
                        System.Console.WriteLine("show all customers");
                        ViewAllCustomers();
                        break;
                    case "7":
                        System.Console.WriteLine("search a customer");
                        ViewCustomerByUsername();
                        break;
                    case "8":
                        System.Console.WriteLine("View all orders");
                        ViewAllOrder();
                        break;
                    case "9":
                        System.Console.WriteLine("exit");
                        repeat = false; 
                        break;
                    default:
                        System.Console.WriteLine("Invalid input");
                        break;
                }

            }while(repeat);
        }

        private void ViewAllOrder()
        {
            List<Order> AllOrder = _orderBL.GetAllOrder();

            foreach(Order order in AllOrder){
                System.Console.WriteLine(order.ToString());
            }
        }

        public void AddLocation(){

            System.Console.WriteLine("what is the name of the location you wnat to add");
            string LocationName = Console.ReadLine();
            System.Console.WriteLine("please input the address of the location");
            string LocationAddress = Console.ReadLine();

            Location newLocation = new Location(LocationName, LocationAddress);

            if (_locationBL.LocationExists(newLocation)) {
                System.Console.WriteLine("location already exists");
            }
            else{
                Location addedLocation = _locationBL.AddLocation(newLocation);
            }

            
        }

        public void ViewAllLocations(){
            List<Location> locations = _locationBL.GetAllLocations();

            if (locations.Count == 0){
                System.Console.WriteLine("there are no locations added");
            }
            else{
                int count = 1;
                foreach(Location loca in locations){
                    System.Console.WriteLine(count + loca.ToString());
                    count += 1;
                }
            }
        }

        public void AddProduct(){
            List<Location> locations = _locationBL.GetAllLocations();
            int amount = locations.Count;

            if (amount == 0){
                System.Console.WriteLine("you dont have any locations. Please add locations");
            }
            else{
                System.Console.WriteLine("What is the name of the product");
                string ProductName = Console.ReadLine();

                System.Console.WriteLine("What is the name of the price of the product");
                string PriceSTR = Console.ReadLine();
                double ProductPrice = Convert.ToDouble(PriceSTR);

                System.Console.WriteLine("How many Products will you store");
                string amountSTR = Console.ReadLine();
                int ProductAmount = Convert.ToInt32(amountSTR);

                System.Console.WriteLine("which location will you be adding it to");
                Location newLocation = GetLocation();

                try{
                    Product newProduct = new Product(ProductName, ProductPrice);
                    Item newItem = new Item(newProduct, ProductAmount);
                    _locationBL.AddItemToLocation(newLocation, newItem);
                }
                catch (Exception ex){
                    Console.WriteLine(ex.Message);
                }
            }

        }

        public void ViewLocationInventory(){
            
            List<Location> locations = _locationBL.GetAllLocations();
            int amount = locations.Count;

            if (amount == 0){
                System.Console.WriteLine("you dont have any locations. Please add locations");
            } 
            else{
                Location location = GetLocation();

                if (location.Inventory.Count == 0){
                    System.Console.WriteLine("You dont have any product at this location");
                }
                else{
                    foreach(Item item in location.Inventory){
                        System.Console.WriteLine(item.ToString());
                    }
                }
                
            }
        }

        public void ReplenishItem(){
            Location location = GetLocation();
            Item item = null;
            string amountStr;
            int amount;
            if (location != null){
                item = GetAnItem(location);
            }
            
            if (item != null){
                System.Console.WriteLine("How many would you like to add");
                amountStr = Console.ReadLine();
                amount = Convert.ToInt32(amountStr);

                _locationBL.ReplenishItem(location, item, amount);
            }
        }

        public Item GetAnItem(Location newLocation){
            List<Item> items = newLocation.Inventory;
            int amount = items.Count;
            int count = 0;
            if (amount == 0){
                System.Console.WriteLine("There are no Items at this Location. Please add a location");
                return null;
            }

            bool repeat = true;
            string StrIndex;
            int index = -1;
            do{
                count = 1;
                foreach(Item item in items){
                    System.Console.WriteLine(count + ": " + item.ToString());
                    count += 1;
                }
                try {
                    System.Console.WriteLine("please enter the index # of the Item you want");
                    StrIndex = Console.ReadLine();
                    index = Convert.ToInt32(StrIndex);
                    index -= 1;
                }
                catch (FormatException){
                    System.Console.WriteLine("please input a number");
                }
                
                if (index >= amount || index < 0){
                    System.Console.WriteLine("wrong input");
                }
                else{
                    repeat = false;
                }
            }while(repeat);

            return items[index];

        }


        public Location GetLocation(){
            List<Location> locations = _locationBL.GetAllLocations();
            int amount = locations.Count;

            if (amount == 0){
                System.Console.WriteLine("There are no Locations recorded please add a location");
                return null;
            }

            bool repeat = true;
            string StrIndex;
            int index = -1;
            do{
                ViewAllLocations();
                try {
                    System.Console.WriteLine("please enter the index # of the location you want");
                    StrIndex = Console.ReadLine();
                    index = Convert.ToInt32(StrIndex);
                    index -= 1;
                }
                catch (FormatException){
                    System.Console.WriteLine("please input a number");
                }
                
                if (index >= amount || index < 0){
                    System.Console.WriteLine("wrong input");
                }
                else{
                    repeat = false;
                }
            }while(repeat);

            return locations[index];
        }
        
        public void ViewAllCustomers(){
            List<Customer> customers = _customerBL.GetAllCustomers();
            int count = 1;
            if (customers.Count == 0){
                System.Console.WriteLine("you have no customers");
            }
            else{
                foreach(Customer customer in customers){
                    System.Console.WriteLine(count + ": "+ customer.ToString());
                    count ++;
                }
            }
        }

        public void ViewCustomerByUsername(){
            System.Console.WriteLine("what is the username of the customer you want to search for");
            string username = Console.ReadLine();

            Customer customer = _customerBL.GetCustomer(username);

            if (customer == null){
                System.Console.WriteLine("There is no customer with that name");
            }
            else{
                System.Console.WriteLine("Customer: " + customer.ToString());
            }
        }
    }
}