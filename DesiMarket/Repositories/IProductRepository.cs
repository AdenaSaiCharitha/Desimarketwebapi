using DesiMarket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesiMarket.Repositories
{
    public interface IProductRepository
    {
        Task<Products> GetProductByIdAsync(int id);
        Task<IEnumerable<Products>> GetProductsAsync();
        Task CreateProductAsync(Products product);
        Task UpdateProductAsync(Products product);
        Task DeleteProductAsync(Products product);

    }
}
