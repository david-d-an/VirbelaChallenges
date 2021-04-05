using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exercise1.Data.Models.VirbelaListing
{
    public partial class ListingRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
