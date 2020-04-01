using Microsoft.AspNetCore.Mvc;

namespace aspnet_core_routing.Controllers
{
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string path)
        {
            return Ok(path);
        }
    }
}
