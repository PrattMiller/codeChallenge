using System;
using NUnit.Framework;
using PME.Sample.Service.Controllers;
using System.Web.Http.Results;

namespace PME.Sample.Service.Test.Controllers
{
    [TestFixture]
    public class TemperatureControllerTest
    {

        [Test]
        public void Get()
        {
            var controller = new TemperatureController();

            var request = new TemperatureController.TemperatureGetRequest
            {
                NumberToReturn = 3,
            };

            var result = controller.Get(request) as JsonResult<TemperatureController.TemperatureGetResponse>;
            Assert.IsNotNull(result);

            var response = result.Content;
            Assert.IsNotNull(response);

            Assert.AreEqual(request.NumberToReturn, response.Items.Count);
        }

    }
}

