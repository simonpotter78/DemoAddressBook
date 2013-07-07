using System;
using System.Web.Mvc;
using smp.AddressBookDemo.Models.Input;
using smp.AddressBookDemo.Repository;
using smp.AddressBookDemo.Web.Filters;

namespace smp.AddressBookDemo.Web.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompanyRepository _repository;

        public CompaniesController(ICompanyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NameStarting(string id)
        {
            var data = _repository.GetCompaniesByFirstLetter(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [GuidId]
        public ActionResult Detail(Guid id)
        {
            var data = _repository.GetCompany(id);
            return View(data);
        }

        [HttpGet]
        public ActionResult AddNew()
        {
            var newId = _repository.AddNew();
            return Json(newId, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(CompanyDetailsInput model)
        {
            var result = _repository.Save(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
