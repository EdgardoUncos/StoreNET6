using System;
using System.Collections.Generic;

namespace Cliente.Models
{
    public partial class OrderStatus

    {
        public OrderStatus()
        {
            CustomerOrder = new HashSet<CustomerOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<CustomerOrder> CustomerOrder { get; set; }
    }
}
