using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadicalECart.Data
{
    public class BillingCheckOutDetails
    {
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

    }
    public class ShippingCheckOutDetails
    {
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

    }
    public class CardDetails
    {
        public string Cvv { get; set; }
        public string ExpYear { get; set; }
        public string Name { get; set; }
        public string ExpMonth { get; set; }
        public string CardNumber { get; set; }
    }
}
