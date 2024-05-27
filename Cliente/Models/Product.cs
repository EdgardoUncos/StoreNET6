using System;
using System.Collections.Generic;

namespace Cliente.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal UnitPrice { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
