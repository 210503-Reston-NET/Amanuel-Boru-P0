using System;
using StoreBL;
using StoreModels;
using StoreDL;
using System.Collections.Generic;

namespace StoreUI
{
    public class CustomerMenu
    {

        private CustomerBL _customerBL;
        private OrderBL _orderBL;
        private Customer _customer;
        private LocationBL _locationBL;

        public CustomerMenu(CustomerBL customerBL, OrderBL newOrder, LocationBL locationBL){
            _customerBL = customerBL; 
            _orderBL = newOrder;
            _locationBL = locationBL;
        }
        public void start(){
            System.Console.WriteLine("Please enter \"1\" if you are an existing customer");
            System.Console.WriteLine("Please enter \"2\" if you are a new customer");
            string response = Console.ReadLine();

            switch(response){
                case "1":
                    System.Console.WriteLine("existing");
                    GetCustomer();
                    break;
                case "2":
                    System.Console.WriteLine("new customer");
                    CreateCustomer();
                    break;
                default:
                    System.Console.WriteLine("Invalid input");
                    break;
            }
        }

        public void CustomerActions(){
            bool repeat = true;

            do{
                System.Console.WriteLine("\nWhat would you like to do.");
                System.Console.WriteLine("Enter 1 to make a new order.");
                System.Console.WriteLine("Enter 2 to view order history by date.");
                System.Console.WriteLine("Enter 3 to view order history by total.");
                System.Console.WriteLine("Enter 4 to go back");
                string response = Console.ReadLine();

                switch(response){
                    case "1":
                        System.Console.WriteLine("new order");
                        MakeAnOrder();
                        break;
                    case "2":
                        System.Console.WriteLine("existing order");
                        viewAllOrdersBydate();
                        break;
                    case "3":
                        System.Console.WriteLine("existing order");
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

        public void CreateCustomer(){
            System.Console.WriteLine("please enter a name");
            string name = Console.ReadLine();
            bool ValidUsername = false;

            System.Console.WriteLine("Please enter a username");
            string GivenUserName = Console.ReadLine();
            bool exists = _customerBL.UserNameExists(GivenUserName);

            if (!exists) ValidUsername = true;

            while(!ValidUsername){
                System.Console.WriteLine("Username is already taken please select a new user name");
                GivenUserName = Console.ReadLine();
                exists = _customerBL.UserNameExists(GivenUserName);
                if (!exists) ValidUsername = true;
            }
            Customer newCustomer = new Customer(name, GivenUserName);

            _customerBL.AddCustomer(newCustomer);
            _customer = newCustomer;
            CustomerActions();

        }

        public void GetCustomer(){
            System.Console.WriteLine("Please enter your username");
            string GivenUserName = Console.ReadLine();
            Customer customer = _customerBL.GetCustomer(GivenUserName);
            bool exists = _customerBL.UserNameExists(GivenUserName);
            

            if (exists){
                System.Console.WriteLine("welcome " + customer.Name);
                _customer = customer;
                CustomerActions();
            }
            else{
                System.Console.WriteLine("the user name does not exist");
            }
        }

        public void MakeAnOrder(){
            try{
                Location location = GetLocation();
                List<Item> items = GetItems(location);
                Order order = new Order(_customer, location, items);
                order.calculateTotal();

                System.Console.WriteLine("Your total is $" + order.Total);
                _orderBL.AddOreder(order, location);
            }
            catch (Exception ex){

                System.Console.WriteLine(ex);
            }
        }

        public void viewAllOrdersBydate(){
            List<Order> orders = _orderBL.CustomerOrdersBydate(_customer);
            System.Console.WriteLine("This are the list of your orders");

            foreach(Order order in orders){
                System.Console.WriteLine(order.ToString());
            }
        }

        public void viewAllOrdersByTotal(){
            List<Order> orders = _orderBL.CustomerOrdersByTotal(_customer);
            System.Console.WriteLine("This are the list of your orders");

            foreach(Order order in orders){
                System.Console.WriteLine(order.ToString());
            }
        }

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
            string response;

            int index = -1;
            int quantity = 0;
            int maxQuantity;

            do{
                viewItems(location);
                index = GetInt("Please enter the index # of the Item you want to buy", 0, amount);
                maxQuantity = inventory[index].Quantity + 1;
                quantity = GetInt("how many Items would you like between 0 and " + (maxQuantity - 1), 0, maxQuantity);
                quantity += 1;
                item = new Item(inventory[index].Product, quantity);
                orders.Add(item);

                System.Console.WriteLine("whould you like to buy more itmes enter \"yes\" to continue or anything else to end purchase");
                response = Console.ReadLine();

                if (response != "yes"){
                    repeat = false;
                }
            }while(repeat);

            return orders;
        }

        public void viewItems(Location location){
            List<Item> items = _locationBL.GetInventory(location);
            int count = 1;

            foreach(Item item in items){
                System.Console.WriteLine(count + ": " + item.ToString());
                count++;
            }
        }
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

        public int GetInt(string request, int minimum, int maximum){
            string StrIndex;
            int index = -1;
            bool repeat = true;
            do{
                try {
                    System.Console.WriteLine(request);
                    StrIndex = Console.ReadLine();
                    index = Convert.ToInt32(StrIndex);
                    index -= 1;
                }
                catch (FormatException){
                    System.Console.WriteLine("please input a number");
                }
                
                if (index >= maximum || index < minimum){
                    System.Console.WriteLine("wrong input");
                }
                else{
                    repeat = false;
                }
            }while(repeat);
            return index;
        }
    }
}