using System.Data;
using smp.QueryDb.Structure;

namespace smp.AddressBookDemo.Repository.Database
{
    public class Db
    {
        public static Company Company = new Company();
        public static Contact Contact = new Contact();
    }

    public class Company : Table
    {
        public Company()
            : base("DemoCompanyAddressBook", "dbo", "Company")
        {
            Id = SetId("Id", DbType.Guid, IdGenerationType.ApplicationGenerated);
            Name = AddColumn("Name", DbType.String, false, 250);
            RegistrationNumber = AddColumn("RegistrationNumber", DbType.String, true, 10);
            Telephone = AddColumn("Telephone", DbType.String, true, 12);
            Email = AddColumn("Email", DbType.String, true, 250);
            WebSite = AddColumn("WebSite", DbType.String, true, 250);
            PostCode = AddColumn("PostCode", DbType.String, false, 8);
            AddressLine1 = AddColumn("AddressLine1", DbType.String, true, 250);
            AddressLine2 = AddColumn("AddressLine2", DbType.String, true, 250);
            Town = AddColumn("Town", DbType.String, true, 250);
            County = AddColumn("County", DbType.String, true, 250);
        }

        public static Id Id;
        public static Column Name;
        public static Column RegistrationNumber;
        public static Column Telephone;
        public static Column Email;
        public static Column WebSite;
        public static Column PostCode;
        public static Column AddressLine1;
        public static Column AddressLine2;
        public static Column Town;
        public static Column County;
    }

    public class Contact : Table
    {
        public Contact()
            : base("DemoCompanyAddressBook", "dbo", "Contact")
        {
            Id = SetId("Id", DbType.Guid, IdGenerationType.ApplicationGenerated);
            CompanyId = AddColumn("CompanyId", DbType.Guid, false);
            Title = AddColumn("Title", DbType.String, false, 10);
            FirstName = AddColumn("FirstName", DbType.String, false, 100);
            LastName = AddColumn("LastName", DbType.String, false, 100);
            Email = AddColumn("Email", DbType.String, true, 250);
            PhoneNumber = AddColumn("PhoneNumber", DbType.String, true, 12);
            CompanyPosition = AddColumn("CompanyPosition", DbType.String, true, 250);
        }

        public static Id Id;
        public static Column CompanyId;
        public static Column Title;
        public static Column FirstName;
        public static Column LastName;
        public static Column Email;
        public static Column PhoneNumber;
        public static Column CompanyPosition;
    }
}
