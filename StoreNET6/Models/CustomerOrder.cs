using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StoreNET6.Models
{
    public partial class CustomerOrder
    {
        public CustomerOrder()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        [Key]
        public int ID { get; set; }
        public int CustomerID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Date { get; set; }
        public int OrderStatusID { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        [ForeignKey("CustomerID")]
        [InverseProperty("CustomerOrder")]
        public virtual Customer Customer { get; set; } = null!;
        [ForeignKey("OrderStatusID")]
        [InverseProperty("CustomerOrder")]
        public virtual OrderStatus OrderStatus { get; set; } = null!;
        [InverseProperty("CustomerOrder")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

    }
        // le agregamos ? a CustomerOrder Product, OrderDetails porque no deja hacer un post pide que es required
}
