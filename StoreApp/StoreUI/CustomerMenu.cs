using System;
using StoreBL;
using StoreModels;
namespace StoreUI
{
    public class CustomerMenu
    {

        private CustomerBL _customerBL;
        private string _userName;

        public CustomerMenu(CustomerBL newStoreBL){
            _customerBL = newStoreBL;
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
            _userName = GivenUserName;

            _customerBL.AddCustomer(new Customer(name, _userName));

        }

        public void GetCustomer(){
            System.Console.WriteLine("Please enter your username");
            string GivenUserName = Console.ReadLine();

            bool exists = _customerBL.UserNameExists(GivenUserName);

            if (exists){
                System.Console.WriteLine("exists");
            }
            else{
                System.Console.WriteLine("the user name does not exist");
            }
        }
    }
}