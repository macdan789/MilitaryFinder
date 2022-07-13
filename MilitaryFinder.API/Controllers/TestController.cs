using Microsoft.AspNetCore.Mvc;

namespace MilitaryFinder.API.Controllers
{
    public class TestController : Controller
    {
        [HttpGet("api/user")]
        public IActionResult Get() => Ok(new { name = "Bohdan" });
    }
}
