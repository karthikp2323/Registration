using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
