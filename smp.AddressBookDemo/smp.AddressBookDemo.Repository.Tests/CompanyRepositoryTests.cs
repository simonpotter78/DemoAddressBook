using System;
using NSubstitute;
using NUnit.Framework;
using smp.AddressBookDemo.Models.Input;
using smp.AddressBookDemo.Models.View;
using smp.AddressBookDemo.Repository.Implementation;
using smp.QueryDb;
using smp.QueryDb.Executing;

namespace smp.AddressBookDemo.Repository.Tests
{
    [TestFixture]
    public class CompanyRepositoryTests
    {
        private IDbSessionFactory _sessionFactory;
        private IDbSession _session;
        private CompanyRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _sessionFactory = Substitute.For<IDbSessionFactory>();
            _session = Substitute.For<IDbSession>();
            _repository = new CompanyRepository(_sessionFactory);
            _sessionFactory.Create().Returns(_session);
            _sessionFactory.Create(true).Returns(_session);
        }

        [Test]
        public void GetCompaniesByFirstLetter_NullProvided_ThrowsException()
        {
            Assert.Throws<Exception>(() => _repository.GetCompaniesByFirstLetter(null));
        }

        [Test]
        public void GetCompaniesByFirstLetter_EmptyProvided_ThrowsException()
        {
            Assert.Throws<Exception>(() => _repository.GetCompaniesByFirstLetter(""));
        }

        [Test]
        public void GetCompaniesByFirstLetter_WhitespaceProvided_ThrowsException()
        {
            Assert.Throws<Exception>(() => _repository.GetCompaniesByFirstLetter("  "));
        }

        [Test]
        public void GetCompaniesByFirstLetter_SessionFactoryThrowsException_Throws()
        {
            _sessionFactory.Create().Returns(x => { throw new Exception(); });
            Assert.Throws<Exception>(() => _repository.GetCompaniesByFirstLetter("A"));
        }

        [Test]
        public void GetCompaniesByFirstLetter_SessionQueryThrowsException_Throws()
        {
            _session.Query(new Query<CompaniesViewItem>()).ReturnsForAnyArgs(x => { throw new Exception(); });
            Assert.Throws<Exception>(() => _repository.GetCompaniesByFirstLetter("A"));
        }

        [Test]
        public void GetCompaniesByFirstLetter_Returns()
        {
            var result = _repository.GetCompaniesByFirstLetter("A");

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetCompany_EmptyGuid_ThrowsException()
        {
            Assert.Throws<Exception>(() => _repository.GetCompany(Guid.Empty));
        }

        [Test]
        public void GetCompany_SessionFactoryThrowsExceotion_Throws()
        {
            _sessionFactory.Create().Returns(x => { throw new Exception(); });
            Assert.Throws<Exception>(() => _repository.GetCompany(Guid.NewGuid()));
        }


        [Test]
        public void GetCompany_SessionLoadThrowsException_Throws()
        {
            _session.Load(new Query<CompanyView>()).ReturnsForAnyArgs(x => { throw new Exception(); });
            Assert.Throws<Exception>(() => _repository.GetCompany(Guid.NewGuid()));
        }

        [Test]
        public void GetCompany_SessionQueryThrowsException_Throws()
        {
            var model = new CompanyView();
            _session.Load(new Query<CompanyView>()).ReturnsForAnyArgs(model);
            _session.Query(new Query<CompanyContactView>()).ReturnsForAnyArgs(x => { throw new Exception(); });
            Assert.Throws<Exception>(() => _repository.GetCompany(Guid.NewGuid()));
        }

        [Test]
        public void GetCompany_Returns()
        {
            var model = new CompanyView();
            _session.Load(new Query<CompanyView>()).ReturnsForAnyArgs(model);
            
            var result = _repository.GetCompany(Guid.NewGuid());
            Assert.IsNotNull(result);
        }

        [Test]
        public void AddNew_SessionFactoryThrowsExceotion_Throws()
        {
            _sessionFactory.Create(true).Returns(x => { throw new Exception(); });
            Assert.Throws<Exception>(() => _repository.AddNew());
        }

        [Test]
        public void AddNew_SessionInsertThrowsExceotion_Throws()
        {
            _sessionFactory.Create().Returns(x => { throw new Exception(); });
            Assert.Throws<Exception>(() => _repository.AddNew());
        }

        [Test]
        public void AddNew_ReturnsNewId()
        {
            _session.Insert(null).ReturnsForAnyArgs(1);
            var result = _repository.AddNew();
            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, result);
        }

        [Test]
        public void Save_SessionFactoryThrowsExceotion_Throws()
        {
            _sessionFactory.Create(true).ReturnsForAnyArgs(x => { throw new Exception(); });
            Assert.Throws<Exception>(() => _repository.Save(new CompanyDetailsInput()));
        }

        [Test]
        public void Save_SessionUpdateThrowsExceotion_Throws()
        {
            _session.Update(null).ReturnsForAnyArgs(x => { throw new Exception(); });
            Assert.Throws<Exception>(() => _repository.Save(new CompanyDetailsInput()));
        }
        [Test]
        public void Save_ReturnsTrue()
        {
            _session.Update(null).ReturnsForAnyArgs(1);
            var result = _repository.Save(new CompanyDetailsInput());
            Assert.IsTrue(result);
        }   
    }
}
