using BackApp.Model;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net;

namespace BackApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EchoController : Controller
    {

        [HttpGet]
        [Route("QueryExtension")]
        public IActionResult Index(
                [FromQuery(Name = "numberValue")] int numberValue, 
                [FromQuery(Name = "message")] string message
            )
        {
            Object obj = new
            {
                message = $"echo de {message.ConcatNewStringForNumber(numberValue)}"
            };

            return StatusCode(StatusCodes.Status200OK, obj);
        }

        [HttpGet]
        [Route("Query")]
        public IActionResult Index([FromQuery(Name = "message")] string messageinput)
        {
            Object obj = new
            {
                message = $"echo de {messageinput}"
            };

            return StatusCode(StatusCodes.Status200OK, obj);
        }

        [HttpGet]
        [Route("Path")]
        public IActionResult IndexEx(string messageinput)
        {
            Object obj = new
            {
                message = $"echo de {messageinput}"
            };

            return StatusCode(StatusCodes.Status200OK, obj);
        }

        [HttpPost]
        [Route("InfoMessage")]
        public IActionResult CreateInfo([FromBody]CMessage messageinput)
        {
            Console.WriteLine($"Auteur: {messageinput.Auteur} code: {messageinput.Code}");

            return StatusCode(StatusCodes.Status202Accepted, "blabla");
        }
    }
}

