using System;

namespace smp.AddressBookDemo.Models.View
{
    public class CompanyContactView
    {
        public Guid ContactId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyPosition { get; set; }
    }
}
