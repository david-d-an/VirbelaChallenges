using System;
using System.Collections.Generic;

namespace Exercise1.Data.Models.VirbelaListing
{
    public partial class Region
    {
        public Region()
        {
            Listinguser = new HashSet<Listinguser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        // Turn off lazy load for performance
        // public virtual ICollection<Listinguser> Listinguser { get; set; }
        public ICollection<Listinguser> Listinguser { get; set; }
    }
}
