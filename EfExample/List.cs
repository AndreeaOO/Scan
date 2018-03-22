using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EfExample
{
    public class List
    {
        [Key]
        [Column("product_name")]
        public string Name { get; set; }
    }
}
