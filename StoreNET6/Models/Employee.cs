using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StoreNET6.Models
{
    public partial class Employee
    {
        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string FirstName { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string LastName { get; set; } = null!;
        [StringLength(32)]
        [Unicode(false)]
        public string UserName { get; set; } = null!;
        [StringLength(32)]
        [Unicode(false)]
        public string Password { get; set; } = null!;
    }
}
