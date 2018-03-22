using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EfExample
{
    public class Products
    {
        [Key]
        [Column("product_code")]
        public double Code { get; set; }
        [Column("product_name")]
        public string Name { get; set; }
    }
}
