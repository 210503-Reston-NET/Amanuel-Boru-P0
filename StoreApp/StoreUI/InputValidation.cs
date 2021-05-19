using System;

namespace StoreUI
{
    public class InputValidation
    {
        public InputValidation()
        {
            
        }

        public string GetString(string message){
            System.Console.WriteLine(message);
            string response = Console.ReadLine();

            while (response.Length == 0){
                System.Console.WriteLine("Input can not be empty");
                System.Console.WriteLine(message);
                response = Console.ReadLine();
            }

            return response;
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

        public double GetDouble(string message){
            string strDouble;
            double response = 0;
            bool repeat = true;

            do{
                try {
                    System.Console.WriteLine(message);
                    strDouble = Console.ReadLine();
                    response = Convert.ToDouble(strDouble);
                    repeat = false;
                }
                catch (FormatException){
                    System.Console.WriteLine("please input a number");
                    repeat = true;
                }
            }while(repeat);

            return response;
        }

        public int GetInt(string message){
            string strInt;
            int response = 0;
            bool repeat = true;

            do{
                try {
                    System.Console.WriteLine(message);
                    strInt = Console.ReadLine();
                    response = Convert.ToInt32(strInt);
                    repeat = false;
                }
                catch (FormatException){
                    System.Console.WriteLine("please input a number");
                    repeat = true;
                }
            }while(repeat);

            return response;
        }

        public bool YesOrNo(string message){
            System.Console.WriteLine(message);
            string response = Console.ReadLine();
            string lowerResponse = response.ToLower();
            bool boolResponse;

            while (lowerResponse != "yes" && lowerResponse != "no"){
                System.Console.WriteLine("Please respond with a yes or no");
                response = Console.ReadLine();
                lowerResponse = response.ToLower();
            }

            if (lowerResponse == "yes"){
                boolResponse = true;
            }
            else{
                boolResponse = false;
            }

            return boolResponse;

        }

        public int OneOrTwo(string request){
            string StrIndex;
            int index = -1;
            bool repeat = true;
            do{
                try {
                    System.Console.WriteLine(request);
                    StrIndex = Console.ReadLine();
                    index = Convert.ToInt32(StrIndex);
                }
                catch (FormatException){
                    System.Console.WriteLine("please input a number");
                }
                
                if (index > 2 || index < 1){
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