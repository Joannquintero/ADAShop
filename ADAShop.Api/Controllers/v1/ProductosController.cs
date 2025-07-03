using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0", Deprecated = true)]
    public class ProductosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Productos desde la versión 1 obsoleto.");
    }
}