using Microsoft.AspNetCore.Mvc;

namespace aspnet_core_routing.Controllers
{
    public class NotDefaultController : ControllerBase
    {
        [HttpGet]
        public IActionResult NotGet(string path)
        {
            return Ok($"This is not {path}");
        }
    }
}
