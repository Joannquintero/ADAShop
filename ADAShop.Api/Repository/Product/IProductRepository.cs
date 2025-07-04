﻿namespace ADAShop.Api.Repository.Product
{
    public interface IProductRepository
    {
        Task<List<Shared.Entities.Product>> GetAllAsync();

        Task<Shared.Entities.Product> GetByIdAsync(int id);

        Task<Shared.Entities.Product> CreateAsync(Shared.Entities.Product product);

        Task<Shared.Entities.Product> UpdateAsync(Shared.Entities.Product product);
    }
}