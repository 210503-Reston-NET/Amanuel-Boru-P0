namespace StoreModels
{
    /// <summary>
    /// This class should contain necessary properties and fields for customer info.
    /// </summary>
    public class Customer
    {
        public Customer(string name, string username){
            this.Name = name;
            this.UserName = username;
        }

        public string Name { get; set; }
        //TODO: add more properties to identify the customer

        public string UserName { get; set;}

        public bool Equals(string username){
            return this.UserName.Equals(username);
        }

        public override string ToString()
        {
            return $" userName: {UserName} \n Name: {Name}";
        }

    }
}