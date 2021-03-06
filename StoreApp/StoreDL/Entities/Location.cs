using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDL.Entities
{
    public partial class Location
    {
        public Location()
        {
            Items = new HashSet<Item>();
            Orders = new HashSet<Order>();
        }

        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
