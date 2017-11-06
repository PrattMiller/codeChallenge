using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using PME.Configuration;

namespace PME.Sample.Service.Controllers
{
    [RoutePrefix("")]
    public class HomeController : ApiController
    {

        private readonly IAppSettings _appSettings;

        public HomeController(
            IAppSettings appSettings
            )
        {
            _appSettings = appSettings;
        }

        [Route("")]
        public HttpResponseMessage Get()
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            var response = new HttpResponseMessage();
            response.Content = new StringContent(
                string.Format(
                    @"
<html>
  <head>
    <title>{0}</title>
    <link rel=""stylesheet"" href=""{1}""/>
  </head>
  <body>
    <div class=""jumbotron"">
      <div class=""container"">
        <h1>{0}</h1>
        <p>{2}</p>
      </div>
    </div>
  </body>
</html>
",
                    "Sample Service",
                    "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css",
                    "v" + version
                ));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

    }
}

