using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDL.Entities
{
    public partial class Item
    {
        public int ItemId { get; set; }
        public int? LocationId { get; set; }
        public int? OrderId { get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }

        public virtual Location Location { get; set; }
        public virtual Order Order { get; set; }
    }
}
