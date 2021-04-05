using System;
using System.Collections.Generic;

namespace Exercise1.Data.Models.VirbelaListing
{
    public partial class ListinguserRequest
    {
        public string Userid { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public int RegionId { get; set; }
    }
}
