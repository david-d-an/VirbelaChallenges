using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exercise1.Data.Models.VirbelaListing
{
    public partial class ListinguserRequest
    {
        [Required]
        public string Userid { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int RegionId { get; set; }
    }
}
