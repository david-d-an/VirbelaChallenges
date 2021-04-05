using System;
using System.Collections.Generic;

namespace Exercise1.Data.Models.VirbelaListing
{
    public partial class ListingRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
