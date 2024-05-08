using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
        [HttpGet("probajson")]
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
            string jsonString = "sikeres json atadas ujra!!!!!";
            string json = JsonSerializer.Serialize(jsonString);
            return this.Content(json, "application/json");
        }
    }
}
