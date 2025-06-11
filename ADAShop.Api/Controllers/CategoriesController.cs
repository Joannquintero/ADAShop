using ADAShop.Api.Repository.Category;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
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
