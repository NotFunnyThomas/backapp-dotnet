using Apache.NMS.ActiveMQ.Commands;
using BackApp.Model;
using BackApp.Model.Repository;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Globalization;
using System.Net;

namespace BackApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EchoController : Controller
    {
        CarRepository _carRepository;

        ILogger<EchoController> _log;

        public EchoController(CarRepository carRepo, ILogger<EchoController> logController)
        {
            _carRepository = carRepo;
            _log = logController;
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
            _log.LogInformation($"message {messageinput}");

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

