using System;
using System.Collections.Generic;
using smp.AddressBookDemo.Models.Input;
using smp.AddressBookDemo.Models.View;

namespace smp.AddressBookDemo.Repository
{
    public interface ICompanyRepository
    {
        CompaniesView GetCompaniesByFirstLetter(string firstLetterOfName);

        CompanyView GetCompany(Guid companyId);

        Guid AddNew();

        bool Save(CompanyDetailsInput model);
    }
}
