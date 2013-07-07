using System;

namespace smp.AddressBookDemo.Models.View
{
    public class CompaniesViewItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PostCode { get; set; }
        public string Town { get; set; }
    }
}
