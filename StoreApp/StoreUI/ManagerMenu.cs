using System;
using System.Collections.Generic;
using StoreBL;
using StoreModels;
using Serilog;
using StoreDL;

namespace StoreUI
{
    public class ManagerMenu
    {
        private LocationBL _locationBL;
        private CustomerBL _customerBL;
        private OrderBL _orderBL;

        private InputValidation _inputValidation;
        
        public ManagerMenu(LocationBL locationbl, CustomerBL customerBL, OrderBL orderBL){
            _locationBL = locationbl;
            _customerBL = customerBL;
            _orderBL = orderBL;
            _inputValidation = new InputValidation();
        }

        /// <summary>
        /// The start of the managers menu
        /// </summary>
        public void start(){
            bool repeat = true;
            do{
                System.Console.WriteLine("\nMANAGER MENU");
                Console.WriteLine("Please enter \"1\" to add a new location");
                Console.WriteLine("Please enter \"2\" to view all locations");
                Console.WriteLine("Please enter \"3\" to view location inventory");
                Console.WriteLine("Please enter \"4\" to add a new product");
                Console.WriteLine("Please enter \"5\" to replenish an item");
                Console.WriteLine("Please enter \"6\" to View all customers");
                Console.WriteLine("Please enter \"7\" to search a customer");
                Console.WriteLine("Please enter \"8\" to view all order");
                Console.WriteLine("Please enter \"9\" to view all order by location");
                Console.WriteLine("Please enter \"10\" Go Back");
                string Response = Console.ReadLine();

                switch(Response){
                    case "1":
                        AddLocation();
                        break;
                    case "2":
                        ViewAllLocations();
                        break;
                    case "3":
                        ViewLocationInventory();
                        break;
                    case "4":
                        AddProduct();
                        break;
                    case "5":
                        ReplenishItem();
                        break;
                    case "6":
                        ViewAllCustomers();
                        break;
                    case "7":
                        ViewCustomerByUsername();
                        break;
                    case "8":
                        ViewAllOrder();
                        break;
                    case "9":
                        ViewOrderByLocation();
                        break;
                    case "10":
                        repeat = false; 
                        break;
                    default:
                        System.Console.WriteLine("Invalid input");
                        break;
                }

            }while(repeat);
        }

        /// <summary>
        /// console prints all of the orders
        /// </summary>
        private void ViewAllOrder()
        {
            List<Order> AllOrder = _orderBL.GetAllOrder();

            foreach(Order order in AllOrder){
                System.Console.WriteLine(order.ToString());
            }
        }

        /// <summary>
        /// this method adds a location to the DB
        /// </summary>
        public void AddLocation(){

            string LocationName = _inputValidation.GetString("what is the name of the location you want to add");
            string LocationAddress = _inputValidation.GetString("please input the address of the location");
            Location newLocation = new Location(LocationName, LocationAddress);

            if (_locationBL.LocationExists(newLocation)) {
                System.Console.WriteLine("location already exists");
            }
            else{
                Location addedLocation = _locationBL.AddLocation(newLocation);
            }

            
        }

        /// <summary>
        /// this displays the locations
        /// </summary>
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

        /// <summary>
        /// adds product to a location
        /// </summary>
        public void AddProduct(){
            List<Location> locations = _locationBL.GetAllLocations();
            int amount = locations.Count;

            if (amount == 0){
                System.Console.WriteLine("you dont have any locations. Please add locations");
            }
            else{
                string ProductName = _inputValidation.GetString("What is the name of the product");
                double ProductPrice = _inputValidation.GetDouble("What is the name of the price of the product");
                int ProductAmount = _inputValidation.GetInt("How many Products will you store");

                System.Console.WriteLine("which location will you be adding it to");
                Location newLocation = GetLocation();

                try{
                    Product newProduct = new Product(ProductName, ProductPrice);
                    Item newItem = new Item(newProduct, ProductAmount);
                    _locationBL.AddItemToLocation(newLocation, newItem);
                }
                catch (Exception ex){
                    Log.Error(ex.Message);
                    Console.WriteLine(ex.Message);
                }
            }

        }

        /// <summary>
        /// this method shows the inventory of a location
        /// </summary>
        public void ViewLocationInventory(){
            
            List<Location> locations = _locationBL.GetAllLocations();
            int amount = locations.Count;

            if (amount == 0){
                System.Console.WriteLine("you dont have any locations. Please add locations");
            } 
            else{
                Location location = GetLocation();
                List<Item> items = _locationBL.GetInventory(location);
                
                if (items.Count == 0){
                    System.Console.WriteLine("you dont have any item at this location");
                }
                else{
                    foreach(Item item in items){
                        Console.WriteLine(item.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// This replenishes the inventory of a location
        /// </summary>
        public void ReplenishItem(){
            Location location = GetLocation();
            Item item = null;
            int amount;
            if (location != null){
                item = GetAnItem(location);
            }
            
            if (item != null){
                amount = _inputValidation.GetInt("How many would you like to add");
                _locationBL.changeInventory(location, item, amount);
            }
        }

        /// <summary>
        /// this allows the user to select an item to get.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public Item GetAnItem(Location location){
            List<Item> items = _locationBL.GetInventory(location);
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

        /// <summary>
        /// Allows the user to select a location to perform functions on
        /// </summary>
        /// <returns></returns>
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
        
        /// <summary>
        /// Shows all the customers
        /// </summary>
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

        /// <summary>
        /// searchs for a customer by name and displays the name
        /// </summary>
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

        /// <summary>
        /// shows orders made at a location
        /// </summary>
        public void ViewOrderByLocation(){
            Location location = GetLocation();
            List<Order> orders;

            if (location == null){
                System.Console.WriteLine("You have no locations to view");
            }
            else{
                int response = _inputValidation.OneOrTwo("Please Enter 1 to sort by date or Enter 2 to sort by total");

                if (response == 1){
                    orders =_orderBL.LocationOrdersBydate(location);
                }
                else{
                    orders =_orderBL.LocationOrdersByTotal(location);
                }

                foreach(Order order in orders){
                    System.Console.WriteLine(order.ToString());
                }
            }
        }
    }
}