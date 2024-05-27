using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StoreNET6.Models
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            CustomerOrder = new HashSet<CustomerOrder>();
        }

        [Key]
        public int ID { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Name { get; set; } = null!;

        [InverseProperty("OrderStatus")]
        public virtual ICollection<CustomerOrder> CustomerOrder { get; set; }
    }
}
