using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using smp.AddressBookDemo.Web.Filters;

namespace smp.AddressBookDemo.Web.Tests
{
    [TestFixture]
    public class GuidIdAttributeTests
    {
        private GuidIdAttribute _attribute = new GuidIdAttribute();

        [Test]
        public void OnActionExecuting_NoParameters_Throws()
        {
            var context = new ActionExecutingContext();
            context.ActionParameters = new SortedList<string, object>();

            Assert.Throws<Exception>(() => _attribute.OnActionExecuting(context));
        }

        [Test]
        public void OnActionExecuting_NoIdParameter_Throws()
        {
            var context = new ActionExecutingContext();
            context.ActionParameters = new SortedList<string, object>();
            context.ActionParameters.Add("notid", "somevalue");

            Assert.Throws<KeyNotFoundException>(() => _attribute.OnActionExecuting(context));
        }

        [Test]
        public void OnActionExecuting_InvalidGuid_Throws()
        {
            var context = new ActionExecutingContext();
            context.ActionParameters = new SortedList<string, object>();
            context.ActionParameters.Add("id", "notaguid");

            Assert.Throws<Exception>(() => _attribute.OnActionExecuting(context));
        }

        [Test]
        public void OnActionExecuting_Passes_GuidId()
        {
            var guidValue = Guid.NewGuid();
            var context = new ActionExecutingContext();
            context.ActionParameters = new SortedList<string, object>();
            context.ActionParameters.Add("id", guidValue);

            _attribute.OnActionExecuting(context);

            Assert.IsTrue(context.ActionParameters["id"] is Guid, "Guid object not applied to filter context");
            Assert.AreEqual(guidValue, context.ActionParameters["id"]);
        }
    }
}
