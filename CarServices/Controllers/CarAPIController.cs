using CarServices.Database;
using CarServices.Database.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarAPIController : ControllerBase
    {
        DatabaseContext db;
        HttpClientHandler _clientHandler = new HttpClientHandler();

        public CarAPIController()
        {
            db = new DatabaseContext();
        }

        [Authorize, HttpGet("list")]
        public IEnumerable<Car> Get()
        {
            var i = 10;
            return db.cars.ToList();
        }

        [Authorize, HttpGet("listofbikefromcar")]
        public async Task<string> GetBikeListfromCar()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            using (var httpclient = new HttpClient(_clientHandler))
            {
                httpclient.DefaultRequestHeaders.Add("Authorization", "" + accessToken);
                using (var response = await httpclient.GetAsync("http://localhost:14493/api/Bike/list"))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    return apiresponse;
                }
            }
        }

        [Authorize, HttpPost("addbikefromcar")]
        public async Task<string> Post([FromBody] CarBike model)
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            using (var httpclient = new HttpClient(_clientHandler))
            {
                httpclient.DefaultRequestHeaders.Add("Authorization", "" + accessToken);
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                using (var response = await httpclient.PostAsync("http://localhost:14493/api/Bike/add", stringContent))
                {
                    return response.StatusCode.ToString();
                }
            }
        }

        [Authorize, HttpPost("add")]
        public IActionResult Post([FromBody] Car model)
        {
            try
            {
                db.cars.Add(model);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}", Name = "Get")]
        public Car Get(string id)
        {
            return db.cars.Find(id);
        }
    }
}
