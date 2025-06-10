using ADAShop.Web.Repository;

namespace ADAShop.Web.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository _repository;

        public CategoryService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Shared.Entities.Category>> GetAllAsync()
        {
            var responseHppt = await _repository.Get<List<Shared.Entities.Category>>($"api/Categories/GetAll");
            return responseHppt.Response!;
        }
    }
}