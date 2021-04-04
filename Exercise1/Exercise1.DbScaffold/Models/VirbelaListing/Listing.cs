using System;
using System.Collections.Generic;

namespace Exercise1.DbScaffold.Models.VirbelaListing
{
    public partial class Listing
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Listinguser Creator { get; set; }
    }
}
