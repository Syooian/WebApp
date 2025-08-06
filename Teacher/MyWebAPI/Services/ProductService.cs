using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.DTOs;
using MyWebAPI.Models;
using MyWebAPI.QueryParameters;

namespace MyWebAPI.Services
{
    public class ProductService
    {
        private readonly GoodStoreContextG2 _context;

        public ProductService(GoodStoreContextG2 context)
        {
            _context = context;
        }

        public async Task<List<ProductDTO>> GetProduct(ProductParam productParam)
        {

            var products = _context.Product.Include(c => c.Cate).OrderBy(p => p.Price).AsQueryable();


            if (!string.IsNullOrEmpty(productParam.catID))
            {

                products = products.Where(p => p.CateID == productParam.catID);

            }

            if (!string.IsNullOrEmpty(productParam.productName))
            {

                products = products.Where(p => p.ProductName.Contains(productParam.productName));
            }

            if (productParam.minPrice.HasValue && productParam.maxPrice.HasValue)
            {

                products = products.Where(p => p.Price >= productParam.minPrice && p.Price <= productParam.maxPrice);
            }


            if (!string.IsNullOrEmpty(productParam.description))
            {

                products = products.Where(p => p.Description.Contains(productParam.description));
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
