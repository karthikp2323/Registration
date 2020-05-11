using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        [Required]
        [Display(Name ="Eamil Id")]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
        public bool AllowEmailNotifications { get; set; }

    }
}
