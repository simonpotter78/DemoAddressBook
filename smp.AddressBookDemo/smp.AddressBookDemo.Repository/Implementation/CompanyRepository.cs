using System;
using System.Collections.Generic;
using smp.AddressBookDemo.Models.Input;
using smp.AddressBookDemo.Models.View;
using smp.AddressBookDemo.Repository.Database;
using smp.QueryDb;
using smp.QueryDb.Executing;

namespace smp.AddressBookDemo.Repository.Implementation
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbSessionFactory _dbSessionFactory;
        
        public CompanyRepository(IDbSessionFactory dbSessionFactory)
        {
            _dbSessionFactory = dbSessionFactory;
        }

        #region Implementation of ICompanyRepository

        public CompaniesView GetCompaniesByFirstLetter(string firstLetterOfName)
        {
            if(string.IsNullOrWhiteSpace(firstLetterOfName))
                throw new Exception("Invalid first letter of company name.");

            var model = new CompaniesView();
            var query = new Query<CompaniesViewItem>()
                    .From(Db.Company)
                    .Map(x => x.Id).To(Company.Id)
                    .Map(x => x.Name).To(Company.Name)
                    .Map(x => x.PostCode).To(Company.PostCode)
                    .Map(x => x.Town).To(Company.Town)
                    .Where(new Filter<CompaniesViewItem>(Company.Name)
                               .Is.StartsWith(firstLetterOfName))
                    .OrderBy(Company.Name);

            using(var session = _dbSessionFactory.Create())
            {
                model.Companies = session.Query(query).Results;
            }
            return model;
        }

        public CompanyView GetCompany(Guid companyId)
        {
            if(companyId == Guid.Empty)
                throw new Exception("Invalid company id");

            var companyQuery = new Query<CompanyView>()
                .From(Db.Company)
                .MapAllProperties().To(Db.Company)
                .Where(new Filter<CompanyView>(Company.Id).Is.EqualTo(companyId));

            var contactsQuery = new Query<CompanyContactView>()
                .From(Db.Contact)
                .MapAllProperties().To(Db.Contact)
                .Map(x => x.ContactId).To(Contact.Id)
                .Where(new Filter<CompanyContactView>(Contact.CompanyId).Is.EqualTo(companyId));

            using(var session = _dbSessionFactory.Create())
            {
                var model = session.Load(companyQuery);
                if (model == null)
                    return null;

                model.Contacts = session.Query(contactsQuery).Results;
                return model;
            }
        }

        public Guid AddNew()
        {
            var newId = Guid.NewGuid();
            
            using(var session = _dbSessionFactory.Create(true))
            {
                var insert = new Insert(Db.Company)
                    .Set(Company.Id).To(newId);

                if(session.Insert(insert) != 1)
                    throw new Exception("Failed to insert new company row");
                session.CommitTransaction();
            }
            return newId;
        }

        public bool Save(CompanyDetailsInput model)
        {
            using(var session = _dbSessionFactory.Create(true))
            {
                var update = new Update(Db.Company)
                    .Set(Company.AddressLine1).To(model.AddressLine1)
                    .Set(Company.AddressLine2).To(model.AddressLine2)
                    .Set(Company.County).To(model.County)
                    .Set(Company.Email).To(model.Email)
                    .Set(Company.Name).To(model.Name)
                    .Set(Company.PostCode).To(model.PostCode)
                    .Set(Company.RegistrationNumber).To(model.RegistrationNumber)
                    .Set(Company.Telephone).To(model.Telephone)
                    .Set(Company.Town).To(model.Town)
                    .Set(Company.WebSite).To(model.WebSite)
                    .Where(new Filter(Company.Id).Is.EqualTo(model.Id));

                if(session.Update(update) != 1)
                    return false;
                session.CommitTransaction();
                return true;
            }
        }

        #endregion
    }
}
