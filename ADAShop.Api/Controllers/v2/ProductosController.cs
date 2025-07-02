using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Api.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("/api/[controller]")]
    public class ProductosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Productos desde la versión 2.");
    }
}