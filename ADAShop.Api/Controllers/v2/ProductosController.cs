using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Api.Controllers.v2
{
    [ApiController] 
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class ProductosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Productos desde la versión 2.");
    }
}