using System.Collections.Generic;
using Model = StoreModels;
using Entity = StoreDL.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StoreDL
{
    public class LocationDB
    {
        private Entity.p0storeContext _context;

        public LocationDB(Entity.p0storeContext context)
        {
            _context = context;
        }

        public Model.Location AddLocation(Model.Location location){
            _context.Locations.Add(
                new Entity.Location{
                    LocationName = location.LocationName,
                    LocationAddress = location.Address
                }
            );

            _context.SaveChanges();
            return location;
        }
        public List<Model.Location> GetAllLocations(){
            return _context.Locations.Select( location => new Model.Location(location.LocationName, location.LocationAddress, location.LocationId)).ToList();
        }

        public Model.Location GetLocation(Model.Location location){
            Entity.Location found = _context.Locations.FirstOrDefault(local =>  local.LocationName == location.LocationName && local.LocationAddress == location.Address);

            if (found == null) return null;
            else{
                return new Model.Location(found.LocationName, found.LocationAddress, found.LocationId);
            }
        }

        public List<Model.Item> GetInventory(Model.Location location){
            Entity.Location found = _context.Locations.FirstOrDefault(local =>  local.LocationName == location.LocationName && local.LocationAddress == location.Address);

            List<Model.Item> items = _context.Items.Where(
                item => item.LocationId == found.LocationId && item.OrderId == null)
                .Select(
                    item => new Model.Item(new Model.Product(item.ProductName, (double)item.Price), (int)item.Quantity)
                ).ToList();

            return items;
        }

        public void changeInventory(Model.Location location, Model.Item item, int amount){
            
                Entity.Item Eitem = _context.Items.
                FirstOrDefault( itm => itm.OrderId == null && itm.LocationId == location.LocationID && itm.ProductName == item.Product.ProductName);

                Eitem.Quantity += amount;

                _context.SaveChanges();
        }

        public Model.Item AddItemToLocation(Model.Location location, Model.Item item){
            _context.Items.Add(
                new Entity.Item{
                    LocationId = location.LocationID,
                    OrderId = null,
                    ProductName = item.Product.ProductName,
                    Price = item.Product.Price,
                    Quantity = item.Quantity
                }
            );
            _context.SaveChanges();
            return item;
        }
    }
}