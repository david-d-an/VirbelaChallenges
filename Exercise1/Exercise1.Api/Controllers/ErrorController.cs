using Microsoft.AspNetCore.Mvc;

namespace Exercise1.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        [HttpGet]
        public IActionResult Error() => Problem();
    }
}