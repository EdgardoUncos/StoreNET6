using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StoreNET6.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        [Key]
        public int ID { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(8, 2)")]
        public decimal UnitPrice { get; set; }

        [InverseProperty("Product")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
