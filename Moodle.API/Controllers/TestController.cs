﻿using Microsoft.AspNetCore.Mvc;

namespace Moodle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        public TestController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [HttpGet]
        public IActionResult GetJsonFile()
        {
            var filepath = Path.Combine(_environment.ContentRootPath, "moodle-test.json");

            if (!System.IO.File.Exists(filepath))
            {
                return NotFound();
            }

            var json = System.IO.File.ReadAllText(filepath);
            return Content(json, "application/json");
        }

        [HttpGet("valami/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok($"Requested data with ID {id}");
        }



        [HttpGet("szamteszt")]
        public IActionResult Szamteszt()
        {
            return Ok($"Sikeresen ossze lett kotve a front és backend");
        }
    }
}
