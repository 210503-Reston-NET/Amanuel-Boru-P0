using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDL.Entities
{
    public partial class Order
    {
        public Order()
        {
            Items = new HashSet<Item>();
        }

        public int OrderId { get; set; }
        public string Cusername { get; set; }
        public DateTime? Orderdate { get; set; }
        public decimal? Total { get; set; }

        public virtual Customer CusernameNavigation { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
