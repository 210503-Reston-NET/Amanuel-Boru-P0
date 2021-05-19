using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StoreBL;
using StoreDL;
using StoreDL.Entities;
using Serilog;

namespace StoreUI
{
    public class MainMenu
    {
        /// <summary>
        /// This is the start of the application. it will present main menue to useers
        /// </summary>
        public void start(){

            Log.Information("Store App started");

            // connecting to the DB
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            string connectionString = configuration.GetConnectionString("StoreDB");
            DbContextOptions<p0storeContext> options = new DbContextOptionsBuilder<p0storeContext>()
            .UseSqlServer(connectionString).Options;

            var context = new p0storeContext(options);

            Console.WriteLine("\n***\tWELCOME TO OUR FLOWER SHOP\t***");
            bool repeat = true;
            do {
                Console.WriteLine("\nMAIN MENU");
                Console.WriteLine("Please enter \"1\" if you are a customer");
                Console.WriteLine("Please enter \"2\" if you are a manager");
                Console.WriteLine("Please enter \"3\" to Exit");
                string Response = Console.ReadLine();

                switch(Response){
                    case "1":
                        CustomerMenu newCustomerMenu = new CustomerMenu(new CustomerBL(new CustomerDB(context) ), new OrderBL(new OrderDB(context)), new LocationBL(new LocationDB(context)));
                        newCustomerMenu.start();
                        break;
                    case "2":
                        ManagerMenu newManager = new ManagerMenu(new LocationBL(new LocationDB(context)), new CustomerBL(new CustomerDB(context) ), new OrderBL(new OrderDB(context)));
                        newManager.start();
                        break;
                    case "3":
                        System.Console.WriteLine("\tThank you for Visiting Our store\n\t\t BYE BYE :)");
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