using System;
using StoreBL;
using StoreDL;

namespace StoreUI
{
    public class MainMenu
    {
        public void start(){
            bool repeat = true;
            do {
                Console.WriteLine("Welcome to the flower shop!!");
                Console.WriteLine("Please enter \"1\" if you are a customer");
                Console.WriteLine("Please enter \"2\" if you are a manager");
                Console.WriteLine("Please enter \"3\" to Exit");
                string Response = Console.ReadLine();

                switch(Response){
                    case "1":
                        System.Console.WriteLine("Customer");
                        CustomerMenu newCustomerMenu = new CustomerMenu(new StoresBL( new StoreRepo()));
                        newCustomerMenu.start();
                        break;
                    case "2":
                        System.Console.WriteLine("manager");
                        break;
                    case "3":
                        System.Console.WriteLine("exit");
                        repeat = false; 
                        break;
                    default:
                        System.Console.WriteLine("Invalid input");
                        break;
                }
            } while(repeat);
        }
    }
}