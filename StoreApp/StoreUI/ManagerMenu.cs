using System;
using System.Collections.Generic;
using StoreBL;
using StoreModels;

namespace StoreUI
{
    public class ManagerMenu
    {
        private LocationBL _locationBL;
        
        public ManagerMenu(LocationBL locationbl){
            _locationBL = locationbl;
        }
        public void start(){
            bool repeat = true;
            do{
                Console.WriteLine("Please enter \"1\" to add a new location");
                Console.WriteLine("Please enter \"2\" to view all location location");
                Console.WriteLine("Please enter \"3\" to view location inventory");
                Console.WriteLine("Please enter \"4\" to add a new product");
                Console.WriteLine("Please enter \"5\" to replenish a new product");
                Console.WriteLine("Please enter \"6\" to View all customers");
                Console.WriteLine("Please enter \"7\" to search a customer");
                Console.WriteLine("Please enter \"8\" Go Back");
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
                        break;
                    case "4":
                        System.Console.WriteLine("add product");
                        break;
                    case "5":
                        System.Console.WriteLine("replenish product");
                        break;
                    case "6":
                        System.Console.WriteLine("show all customers");
                        break;
                    case "7":
                        System.Console.WriteLine("search a customer");
                        break;
                    case "8":
                        System.Console.WriteLine("exit");
                        repeat = false; 
                        break;
                    default:
                        System.Console.WriteLine("Invalid input");
                        break;
                }

            }while(repeat);
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
                foreach(Location loca in locations){
                    System.Console.WriteLine(loca.ToString());
                }
            }
        }
    }
}