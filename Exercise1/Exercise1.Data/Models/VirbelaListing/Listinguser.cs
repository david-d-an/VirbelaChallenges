using System;
using System.Collections.Generic;

namespace Exercise1.Data.Models.VirbelaListing
{
    public partial class Listinguser
    {
        public Listinguser()
        {
            Listing = new HashSet<Listing>();
        }

        public int Id { get; set; }
        public string Userid { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public int RegionId { get; set; }

        // Turn off lazy load for performance
        // public virtual Region Region { get; set; }
        // public virtual ICollection<Listing> Listing { get; set; }
        public Region Region { get; set; }
        public ICollection<Listing> Listing { get; set; }
    }
}
