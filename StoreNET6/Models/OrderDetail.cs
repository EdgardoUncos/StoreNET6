using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StoreNET6.Models
{
    public partial class OrderDetail
    {
        [Key]
        public int CustomerOrderID { get; set; }
        [Key]
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        [ForeignKey("CustomerOrderID")]
        [InverseProperty("OrderDetail")]
        public virtual CustomerOrder CustomerOrder { get; set; } = null!;
        [ForeignKey("ProductID")]
        [InverseProperty("OrderDetail")]
        public virtual Product Product { get; set; } = null!;
    }

    
}
