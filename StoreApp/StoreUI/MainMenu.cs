using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StoreBL;
using StoreDL;
using StoreDL.Entities;

namespace StoreUI
{
    public class MainMenu
    {
        public void start(){
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            string connectionString = configuration.GetConnectionString("StoreDB");

            DbContextOptions<p0storeContext> options = new DbContextOptionsBuilder<p0storeContext>()
            .UseSqlServer(connectionString).Options;

            var context = new p0storeContext(options);

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
                        CustomerMenu newCustomerMenu = new CustomerMenu(new CustomerBL(new CustomerDB(context) ), new OrderBL(new OrderDB(context)), new LocationBL(new LocationDB(context)));
                        newCustomerMenu.start();
                        break;
                    case "2":
                        System.Console.WriteLine("manager");
                        ManagerMenu newManager = new ManagerMenu(new LocationBL(new LocationDB(context)), new CustomerBL(new CustomerDB(context) ), new OrderBL(new OrderDB(context)));
                        newManager.start();
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