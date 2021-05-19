using System;
using StoreBL;
using StoreModels;
using StoreDL;
using System.Collections.Generic;
using Serilog;

namespace StoreUI
{
    public class CustomerMenu
    {

        private CustomerBL _customerBL;
        private OrderBL _orderBL;
        private Customer _customer;
        private LocationBL _locationBL;
        private InputValidation _inputValidation;

        public CustomerMenu(CustomerBL customerBL, OrderBL newOrder, LocationBL locationBL){
            _customerBL = customerBL; 
            _orderBL = newOrder;
            _locationBL = locationBL;
            _inputValidation = new InputValidation();
        }
        public void start(){
            System.Console.WriteLine("\nCUSTOMER MENU");
            System.Console.WriteLine("Please enter \"1\" if you are an existing customer");
            System.Console.WriteLine("Please enter \"2\" if you are a new customer");
            string response = Console.ReadLine();

            switch(response){
                case "1":
                    GetCustomer();
                    break;
                case "2":
                    CreateCustomer();
                    break;
                default:
                    System.Console.WriteLine("Invalid input");
                    break;
            }
        }

        /// <summary>
        /// Once Customers are signed in they will be able to perform using this method
        /// </summary>
        public void CustomerActions(){
            bool repeat = true;
            System.Console.WriteLine("\nwelcome " + _customer.Name);
            do{
                System.Console.WriteLine("\nWhat would you like to do.");
                System.Console.WriteLine("Enter 1 to make a new order.");
                System.Console.WriteLine("Enter 2 to view order history by date.");
                System.Console.WriteLine("Enter 3 to view order history by total.");
                System.Console.WriteLine("Enter 4 to go back");
                string response = Console.ReadLine();

                switch(response){
                    case "1":
                        MakeAnOrder();
                        break;
                    case "2":
                        viewAllOrdersBydate();
                        break;
                    case "3":
                        viewAllOrdersByTotal();
                        break;
                    case "4":
                        repeat = false;
                        break;
                    default:
                        System.Console.WriteLine("Invalid input");
                        break;
                }
            }while(repeat);
        }

        /// <summary>
        /// This Method adds a new customer to the data base
        /// </summary>
        public void CreateCustomer(){
            string name = _inputValidation.GetString("please enter a name");
            bool ValidUsername = false;

            string GivenUserName = _inputValidation.GetString("Please enter a username");
            bool exists = _customerBL.UserNameExists(GivenUserName);

            if (!exists) ValidUsername = true;

            while(!ValidUsername){
                GivenUserName = _inputValidation.GetString("Username is already taken please select a new user name");
                exists = _customerBL.UserNameExists(GivenUserName);
                if (!exists) ValidUsername = true;
            }
            Customer newCustomer = new Customer(name, GivenUserName);

            Log.Information("New Customer added");
            _customerBL.AddCustomer(newCustomer);
            _customer = newCustomer;
            CustomerActions();

        }

        /// <summary>
        /// This method gets a customer from the data base
        /// </summary>
        public void GetCustomer(){
            string GivenUserName = _inputValidation.GetString("Please enter your username");
            Customer customer = _customerBL.GetCustomer(GivenUserName);
            bool exists = _customerBL.UserNameExists(GivenUserName);

            if (exists){
                Log.Information("Customer signed in");
                _customer = customer;
                CustomerActions();
            }
            else{
                System.Console.WriteLine("The user name does not exist");
            }
        }

        /// <summary>
        /// this method lets the user make an order to the user
        /// </summary>
        public void MakeAnOrder(){
            try{
                Location location = GetLocation();
                List<Item> items = GetItems(location);
                Order order = new Order(_customer, location, items);
                order.calculateTotal();

                foreach(Item item in items){
                    System.Console.WriteLine("\t" + item.ToString());
                }

                System.Console.WriteLine("Your total is $" + order.Total);
                _orderBL.AddOreder(order, location);
            }
            catch (Exception ex){
                Log.Error(ex.Message);
                System.Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// This allows customers to view their past orders sorted by date
        /// </summary>
        public void viewAllOrdersBydate(){
            List<Order> orders = _orderBL.CustomerOrdersBydate(_customer);
            System.Console.WriteLine("This is a list of your orders sorted by date");

            foreach(Order order in orders){
                System.Console.WriteLine(order.ToString());
            }
        }

        /// <summary>
        /// This allows customers to view their past orders sorted by total 
        /// </summary>
        public void viewAllOrdersByTotal(){
            List<Order> orders = _orderBL.CustomerOrdersByTotal(_customer);
            System.Console.WriteLine("This is a list of your orders sorted by Total");

            foreach(Order order in orders){
                System.Console.WriteLine(order.ToString());
            }
        }

        /// <summary>
        /// this method gets all the Items in a location
        /// </summary>
        /// <param name="location"> Location object</param>
        /// <returns></returns>
        private List<Item> GetItems(Location location)
        {
            List<Item> inventory = _locationBL.GetInventory(location);
            int amount = inventory.Count;

            if (amount == 0){
                throw new Exception("This location doesnt have any inventory");
            }

            bool repeat = true;
            List<Item> orders = new List<Item>();
            Item item;

            int index = -1;
            int quantity = 0;
            int maxQuantity;

            do{
                viewItems(inventory);
                index = _inputValidation.GetInt("Please enter the index # of the Item you want to buy", 0, amount);
                maxQuantity = inventory[index].Quantity + 1;
                quantity = _inputValidation.GetInt("how many Items would you like between 0 and " + (maxQuantity - 1), 0, maxQuantity);
                quantity += 1;
                inventory[index].Quantity -= quantity;
                item = new Item(inventory[index].Product, quantity);
                orders.Add(item);

                repeat = _inputValidation.YesOrNo("whould you like to buy more itmes enter \"yes\" to continue or \"no\" to end purchase");
            }while(repeat);

            return orders;
        }

        /// <summary>
        /// console writes all the items to be seen by the user
        /// </summary>
        /// <param name="Items"></param>
        public void viewItems(List<Item> Items){
            List<Item> items = Items;
            int count = 1;

            foreach(Item item in items){
                System.Console.WriteLine(count + ": " + item.ToString());
                count++;
            }
        }

        /// <summary>
        /// gets all the locations Available
        /// </summary>
        /// <returns></returns>
        public Location GetLocation(){
            List<Location> locations = _locationBL.GetAllLocations();
            int amount = locations.Count;

            if (amount == 0){
                System.Console.WriteLine("There are no Locations available come back letter");
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
        /// shows all the available locations to the user
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
    }
}