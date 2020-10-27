using System;

namespace BoilerplatePro.nuDirect
{
    public class CustomerEntities
    {
        public int CustomerId { get; set; }

        public string Company { get; set; }

        public ContactEntities[] Contacts { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string BusinessUnit { get; set; }

    }
}