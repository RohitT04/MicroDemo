using Auth.Models;
using BikeService.Database;
using BikeService.Database.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BikeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class BikeController : ControllerBase
    {
        DatabaseContext db;
        HttpClientHandler _clientHandler = new HttpClientHandler();
        public BikeController()
        {
            db = new DatabaseContext();
        }

        [HttpGet("Admins")]
        [Authorize]//(Roles = "Administrator")]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.GivenName}, you are an {currentUser.Role}");
        }

        [Authorize, HttpGet("list")]
        public IEnumerable<Bike> Get()
        {
            return db.Bikes.ToList();
        }

        [Authorize, HttpGet("list1")]
        public async Task<string> Get1()
        {
            using (var httpclient = new HttpClient(_clientHandler))
            {
                var accesstoken = Request.Headers[HeaderNames.Authorization];
                httpclient.DefaultRequestHeaders.Add("Authorization", "" + accesstoken);
                using (var response = await httpclient.GetAsync("http://localhost:50792/api/CarAPI/list"))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    return apiresponse;
                }
            }
        }

        [Authorize, HttpPost("add")]
        public IActionResult Post([FromBody] Bike model)
        {
            try
            {
                db.Bikes.Add(model);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserModel
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    GivenName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}
