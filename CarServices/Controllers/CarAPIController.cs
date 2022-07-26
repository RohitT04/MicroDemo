using CarServices.Database;
using CarServices.Database.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JwtApp.Models;
using JwtApp.Controllers;
using System.Security.Claims;

namespace CarServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CarAPIController : ControllerBase
    {
        DatabaseContext db;

        public CarAPIController()
        {
             db = new DatabaseContext();
        }

        [HttpGet("list")]
        [Authorize]
        public IEnumerable<Car> Get()
        {
            return db.cars.ToList();
        }

        [HttpPost]

        public IActionResult Post([FromBody] Car model)
        {
            try
            {
                db.cars.Add(model);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}", Name ="Get")]

        public Car Get(string id)
        {
            return db.cars.Find(id);
        }
    }
}
