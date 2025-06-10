using ADAShop.Api.Repository.Product;
using ADAShop.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ADAShop.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet(nameof(GetAll))]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll()
        {
            var response = await _productRepository.GetAllAsync();
            return Ok(response);
        }

        [HttpGet(nameof(GetById))]
        [AllowAnonymous]
        public async Task<ActionResult> GetById(int id)
        {
            var response = await _productRepository.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPost(nameof(Insert))]
        public async Task<ActionResult> Insert(Product product)
        {
            var response = await _productRepository.CreateAsync(product);
            return Ok(response);
        }

        [HttpPost(nameof(Update))]
        public async Task<ActionResult> Update(Product product)
        {
            var response = await _productRepository.UpdateAsync(product);
            return Ok(response);
        }
    }
}