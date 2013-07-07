using System;
using System.Collections.Generic;

namespace smp.AddressBookDemo.Models.View
{
    public class CompanyView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string PostCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public List<CompanyContactView> Contacts { get; set; }
    }
}
