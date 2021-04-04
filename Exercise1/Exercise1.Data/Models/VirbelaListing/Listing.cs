using System;
using System.Collections.Generic;

namespace Exercise1.Data.Models.VirbelaListing
{
    public partial class Listing
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreatedDate { get; set; }

        // Turn off lazy load for performance
        // public virtual Listinguser Creator { get; set; }
        public Listinguser Creator { get; set; }
    }
}
