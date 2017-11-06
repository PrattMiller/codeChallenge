using PME.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PME.Sample.Service.Controllers
{
    [RoutePrefix("temp")]
    public class TemperatureController : ApiController 
    {

        private List<Forecast> _items = new List<Forecast>();

        public class Forecast
        {
            public DateTime Timestamp { get; set; }
            public Temperature Temperature { get; set; }
        }

        public class TemperatureGetRequest
        {
            public int NumberToReturn { get; set; }
        }

        public class TemperatureGetResponse
        {
            public bool Success { get; set; }

            public List<Forecast> Items { get; set; }
        }

        public class TemperaturePostRequest
        {
            public DateTime Timestamp { get; set; }
            public int Temperature { get; set; }

        }

        public class TemperaturePostResponse
        {
            public bool Success { get; set; }
        }

        public TemperatureController()
        {
            _items.Add(new Forecast { Timestamp = DateTime.Parse("11/06/2017 09:00"), Temperature = Temperature.FromFahrenheit(60) });
            _items.Add(new Forecast { Timestamp = DateTime.Parse("11/06/2017 09:30"), Temperature = Temperature.FromFahrenheit(62) });
            _items.Add(new Forecast { Timestamp = DateTime.Parse("11/06/2017 10:00"), Temperature = Temperature.FromFahrenheit(65) });
            _items.Add(new Forecast { Timestamp = DateTime.Parse("11/06/2017 10:30"), Temperature = Temperature.FromFahrenheit(68) });
            _items.Add(new Forecast { Timestamp = DateTime.Parse("11/06/2017 11:00"), Temperature = Temperature.FromFahrenheit(69) });
            _items.Add(new Forecast { Timestamp = DateTime.Parse("11/06/2017 11:30"), Temperature = Temperature.FromFahrenheit(70) });
            _items.Add(new Forecast { Timestamp = DateTime.Parse("11/06/2017 12:00"), Temperature = Temperature.FromFahrenheit(71) });
            _items.Add(new Forecast { Timestamp = DateTime.Parse("11/06/2017 13:00"), Temperature = Temperature.FromFahrenheit(74) });

        }

        [Route("")]
        public IHttpActionResult Get([FromBody] TemperatureGetRequest temperatureRequest)
        {

            var temperatureResponse = new TemperatureGetResponse
            {
                Items = _items
                    .Take(temperatureRequest.NumberToReturn - 1)
                    .ToList()
            };
            
            return Json(temperatureResponse);
        }

        [Route("celsius")]
        public IHttpActionResult GetAsCelsius([FromBody] TemperatureGetRequest temperatureRequest)
        {

            var temperatureResponse = new TemperatureGetResponse
            {
                Items = _items
                    .Take(temperatureRequest.NumberToReturn - 1)
                    .ToList()
            };

            // TODO: Need to convert the temps to celsius...

            return Json(temperatureResponse);
        }

        [Route("")]
        public IHttpActionResult Post([FromBody] TemperaturePostRequest temperatureRequest)
        {
            _items.Add(new Forecast
            {
                Timestamp = temperatureRequest.Timestamp,
                Temperature = temperatureRequest.Temperature
            }
            );

            var response = new TemperaturePostResponse
            {

            };

            return Json(response);
        }


    }
}

