using System;
using System.Collections.Generic;

namespace Exercise1.DbScaffold.Models.VirbelaListing
{
    public partial class Region
    {
        public Region()
        {
            Listinguser = new HashSet<Listinguser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Listinguser> Listinguser { get; set; }
    }
}
