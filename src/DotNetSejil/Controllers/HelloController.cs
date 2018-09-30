using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetSejil {

    public class HelloRequest {
        [Required]
        public string From { set; get; }
        [Required]
        public string Message { set; get; }
    }

    public class HelloResponse {
        public DateTime Date { set; get; }
        public string Message { set; get; }
    }

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HelloController : ControllerBase {
        private readonly ILogger<HelloController> logger;

        public HelloController(ILogger<HelloController> logger) {
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<HelloResponse> Hi() {
            return new HelloResponse { };
        }

        [HttpPost]
        public ActionResult<HelloResponse> Hi(HelloRequest request) {
            if (request.From != "bot") {
                logger.LogInformation("New request - {@Request}", request);
                return new HelloResponse {
                    Message = $"Hi {request.From}",
                    Date = DateTime.Now
                };
            }

            logger.LogError("Bad reqeust - {@Request}", request);
            return BadRequest(request);
        }
    }
}