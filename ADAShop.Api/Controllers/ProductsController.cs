using ADAShop.Api.Repository.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll()
        {
            var response = await _productRepository.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("GetById")]
        [AllowAnonymous]
        public async Task<ActionResult> GetById(int id)
        {
            var response = await _productRepository.GetByIdAsync(id);
            return Ok(response);
        }
    }
}