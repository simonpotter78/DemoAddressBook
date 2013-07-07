using System;
using System.Web.Mvc;

namespace smp.AddressBookDemo.Web.Filters
{
    public class GuidIdAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var parameters = filterContext.ActionParameters;
            if (parameters.Count != 1)
            {
                throw new Exception("Invalid paramenter use.");
            }
            var id = parameters["id"];
            if (id == null)
            {
                throw new Exception("Null id is invalid for action method call.");
            }
            Guid value;
            if (id is string)
            {

                if (!Guid.TryParse(id.ToString(), out value))
                {
                    throw new Exception("Invalid id value used : " + id);
                }
            }
            else
            {
                value = (Guid)id;
            }
            filterContext.ActionParameters.Clear();
            filterContext.ActionParameters.Add("id", value);
            base.OnActionExecuting(filterContext);
        }
    }
}