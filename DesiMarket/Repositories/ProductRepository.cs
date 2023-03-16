using DesiMarket.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesiMarket.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OnlineGroceryDbContext _dbContext;
        public ProductRepository(OnlineGroceryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Products> GetProductByIdAsync(int id)
        {
            return await _dbContext.Product.FindAsync(id);
        }
        public async Task<IEnumerable<Products>> GetProductsAsync()
        {
            return await _dbContext.Product.ToListAsync();
        }

        public async Task CreateProductAsync(Products product)
        {
            _dbContext.Product.Add(product);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateProductAsync(Products product)
        {
            _dbContext.Product.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Products product)
        {
            _dbContext.Product.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
