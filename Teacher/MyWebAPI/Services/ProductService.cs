using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.DTOs;
using MyWebAPI.Models;

namespace MyWebAPI.Services
{
    public class ProductService
    {
        private readonly GoodStoreContextG2 _context;

        public ProductService(GoodStoreContextG2 context)
        {
            _context = context;
        }

        public async Task<List<ProductDTO>> GetProduct(string? cateID, string? productName, decimal? minPrice, decimal? maxPrice, string? description)
        {

            var products = _context.Product.Include(c => c.Cate).OrderBy(p => p.Price).AsQueryable();


            if (!string.IsNullOrEmpty(cateID))
            {

                products = products.Where(p => p.CateID == cateID);

            }

            if (!string.IsNullOrEmpty(productName))
            {

                products = products.Where(p => p.ProductName.Contains(productName));
            }

            if (minPrice.HasValue && maxPrice.HasValue)
            {

                products = products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
            }


            if (!string.IsNullOrEmpty(description))
            {

                products = products.Where(p => p.Description.Contains(description));
            }

            var productsList =  await products.Select(p => ItemProduct(p)).ToListAsync();

            return productsList;
        }

        public async Task<ProductDTO> GetProduct(string id)
        {
            var product = await _context.Product.Include(c => c.Cate).Where(p => p.ProductID == id)
              .OrderBy(p => p.Price).Select(p => ItemProduct(p)).FirstOrDefaultAsync();


            return product;
        }

        private static ProductDTO ItemProduct(Product p)
        {
            var result = new ProductDTO
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                Price = p.Price,
                Description = p.Description,
                Picture = p.Picture,
                CateID = p.CateID,
                CateName = p.Cate.CateName

            };

            return result;

        }
    }
}
