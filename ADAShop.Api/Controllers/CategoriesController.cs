using ADAShop.Api.Repository.Category;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("/api/[controller]")]
    [ApiVersion("1.0")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult> GetAll()
        {
            var response = await _categoryRepository.GetAllAsync();
            return Ok(response);
        }
    }
}