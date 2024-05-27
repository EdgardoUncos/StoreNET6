using System;
using System.Collections.Generic;

namespace Cliente.Models
{
    public partial class CustomerOrder
    {
        public CustomerOrder()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public int OrderStatusId { get; set; }
        public decimal Amount { get; set; }

        public virtual Customer? Customer { get; set; } = null!;
        public virtual OrderStatus? OrderStatus { get; set; } = null!;
        public virtual ICollection<OrderDetail>? OrderDetail { get; set; }
    }
}
