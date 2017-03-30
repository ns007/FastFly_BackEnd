using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastFly.BeckEnd.Models
{
    public class UserModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> Faculty { get; set; }
        public Nullable<int> Department { get; set; }
        public string Role { get; set; }
        public Nullable<int> PercentageJob { get; set; }
        public string CellNum { get; set; }
        public Nullable<int> ApplicationRole { get; set; }
    }
}