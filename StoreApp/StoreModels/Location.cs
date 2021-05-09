namespace StoreModels
{
    /// <summary>
    /// This class should contain all the fields and properties that define a store location.
    /// </summary>
    public class Location
    {
        public Location(string locationName , string address){
            Address = address;
            LocationName = locationName;
        }
        public string Address { get; set; }
        public string LocationName { get; set; }
        //TODO: add some property for the location inventory

        public bool Equals(Location newLocation){
            return this.Address.Equals(newLocation.Address) && this.LocationName.Equals(newLocation.LocationName);
        }

        public override string ToString()
        {
            return $" Location name: {LocationName} \n\t Address: {Address}";
        }
    }
}