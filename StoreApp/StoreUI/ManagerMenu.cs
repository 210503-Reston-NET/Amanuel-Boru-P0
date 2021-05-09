using System;
using StoreBL;
using StoreModels;

namespace StoreUI
{
    public class ManagerMenu
    {

        public void start(){
            bool repeat = true;
            do{

                Console.WriteLine("Welcome to the flower shop!!");
                Console.WriteLine("Please enter \"1\" to add a new location");
                Console.WriteLine("Please enter \"2\" to add a new product");
                Console.WriteLine("Please enter \"3\" to replenish a new product");
                Console.WriteLine("Please enter \"4\" to search a customer");
                Console.WriteLine("Please enter \"5\" Go Back");
                string Response = Console.ReadLine();

                switch(Response){
                    case "1":
                        System.Console.WriteLine("add location");
                        break;
                    case "2":
                        System.Console.WriteLine("add product");
                        break;
                    case "3":
                        System.Console.WriteLine("replenish product");
                        break;
                    case "4":
                        System.Console.WriteLine("search a customer");
                        break;
                    case "5":
                        System.Console.WriteLine("exit");
                        repeat = false; 
                        break;
                    default:
                        System.Console.WriteLine("Invalid input");
                        break;
                }

            }while(repeat);
        }
    }
}