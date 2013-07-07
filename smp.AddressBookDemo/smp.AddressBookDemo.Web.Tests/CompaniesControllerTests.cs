using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NSubstitute;
using NUnit.Framework;
using smp.AddressBookDemo.Models.Input;
using smp.AddressBookDemo.Models.View;
using smp.AddressBookDemo.Repository;
using smp.AddressBookDemo.Web.Controllers;

namespace smp.AddressBookDemo.Web.Tests
{
    [TestFixture]
    public class CompaniesControllerTests
    {
        private ICompanyRepository _repository;
        private CompaniesController _controller;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            _repository = Substitute.For<ICompanyRepository>();
            _controller = new CompaniesController(_repository);
        }

        [Test]
        public void Index_ReturnsView()
        {
            var result = _controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void NameStarting_ThrowsException_Throws()
        {
            _repository.GetCompaniesByFirstLetter("A").ReturnsForAnyArgs(x => { throw new Exception(); });
            Assert.Throws<Exception>(() => _controller.NameStarting("A"));
        }

        [Test]
        public void NameStarting_Returns()
        {
            _repository.GetCompaniesByFirstLetter("A").Returns(new CompaniesView());
            var result = _controller.NameStarting("A");
            Assert.IsNotNull(result);
            Assert.IsNotNull(((JsonResult) result).Data);
        }

        [Test]
        public void Detail_ThrowsException_Throws()
        {
            var id = Guid.NewGuid();
            _repository.GetCompany(id).ReturnsForAnyArgs(x => { throw new Exception(); });
            Assert.Throws<Exception>(() => _controller.Detail(id));
        }

        [Test]
        public void Detail_Returns()
        {
            var id = Guid.NewGuid();
            _repository.GetCompany(id).Returns(new CompanyView());
            var result = _controller.Detail(id);
            Assert.IsNotNull(result);
        }

        [Test]
        public void AddNew_ThrowsException_Throws()
        {
            _repository.AddNew().ReturnsForAnyArgs(x => { throw new Exception(); });
            Assert.Throws<Exception>(() => _controller.AddNew());
        }

        [Test]
        public void AddNew_Returns()
        {
            var newId = Guid.NewGuid();
            _repository.AddNew().ReturnsForAnyArgs(newId);
            var result = _controller.AddNew();
            Assert.IsNotNull(result);
            Assert.IsNotNull(((JsonResult)result).Data);
            Assert.AreEqual(newId.ToString(), ((JsonResult)result).Data.ToString());
        }

        [Test]
        public void Save_ThrowsException_Throws()
        {
            _repository.Save(new CompanyDetailsInput()).ReturnsForAnyArgs(x => { throw new Exception(); });
            Assert.Throws<Exception>(() => _controller.Save(new CompanyDetailsInput()));
        }

        [Test]
        public void Save_Returns()
        {
            _repository.Save(new CompanyDetailsInput()).ReturnsForAnyArgs(true);
            var result = _controller.Save(new CompanyDetailsInput());
            Assert.IsNotNull(result);
            Assert.IsNotNull(((JsonResult)result).Data);
            Assert.AreEqual("True", ((JsonResult)result).Data.ToString());
        }
    }
}
