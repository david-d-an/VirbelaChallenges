using System;

namespace Exercise1.Data.Models.VirbelaListing
{
    public  class Region_Listing
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
    }

}
