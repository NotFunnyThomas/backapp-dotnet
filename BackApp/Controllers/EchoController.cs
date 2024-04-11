using BackApp.Model;
using BackApp.Model.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net;

namespace BackApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EchoController : Controller
    {
        CarRepository _carRepository;

        public EchoController()
        {
            _carRepository = new CarRepository();
        }

        [HttpPost]
        [Route("ExecDapper")]
        public IActionResult ExecWriteDapper([FromBody] Car car)
        {
            String result = _carRepository.Execute(car);
            return StatusCode(StatusCodes.Status200OK, result);
        }



        [HttpGet]
        [Route("ExecDapper")]
        public IActionResult ExecDapper()
        {
            String version = _carRepository.Execute();
            return StatusCode(StatusCodes.Status200OK, version);
        }

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

