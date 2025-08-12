using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductFromSQL(ProductParam productParam)
        {


            string sql = "select p.ProductID, p.ProductName, p.Price, p.Description, p.Picture, p.CateID, c.CateName " +
                " from Product as p inner join Category as c on p.CateID=c.CateID where 1=1 ";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(productParam.catID))
            {
             
                sql += " and p.CateID = @cate ";
                parameters.Add(new SqlParameter("@cate", productParam.catID));
            }

            if (!string.IsNullOrEmpty(productParam.productName))
            {
             
                sql += " and p.ProductName like @productName ";
                parameters.Add(new SqlParameter("@productName", $"%{productParam.productName}%"));
            }

            if (productParam.minPrice.HasValue && productParam.maxPrice.HasValue)
            {
             
                sql += " and p.Price between @minPrice and @maxPrice ";
                parameters.Add(new SqlParameter("@minPrice", productParam.minPrice));
                parameters.Add(new SqlParameter("@maxPrice", productParam.maxPrice));
            }


            if (!string.IsNullOrEmpty(productParam.description))
            {
               
                sql += $" and p.Description like @description ";
                parameters.Add(new SqlParameter("@description", $"%{productParam.description}%"));
            }

         
            var products = await _context.ProductDTO.FromSqlRaw(sql, parameters.ToArray()).ToListAsync();

       

            return products;
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
