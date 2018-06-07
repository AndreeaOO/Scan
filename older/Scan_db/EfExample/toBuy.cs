using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Scan
{
    public class To_Buy
    {
        [Key] 
        [Column("product_name")]
        public string Name { get; set; }
    }
}
