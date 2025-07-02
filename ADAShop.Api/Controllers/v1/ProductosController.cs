using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/[controller]")]
    public class ProductosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Productos desde la versión 1.");
    }
}