using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECom.ReadModel;

namespace ECom.Site.Areas.Shop.Models
{
    public class AddressViewModel
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
    }
}