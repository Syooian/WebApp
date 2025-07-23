using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPITest.Data;
using WebAPITest.DTOs;
using WebAPITest.Models;

namespace WebAPITest.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly GoodStoreContext _context;

        public ProductsController(GoodStoreContext context)
        {
            _context = context;
        }

        // GET: api/Products
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CateID"></param>
        /// <param name="ProductName">商品名稱</param>
        /// <param name="MaxPrice">最高價格</param>
        /// <param name="MinPrice">最低價格</param>
        /// <param name="Description">商品描述</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts(string? CateID, string? ProductName, decimal? MaxPrice, decimal? MinPrice, string? Description)
        {
            var Products = _context.Product.Include(C => C.Cate).OrderBy(P => P.Price).AsQueryable();

            //產品類別搜尋
            if (!string.IsNullOrEmpty(CateID))
                Products = Products.Where(P => P.CateID == CateID);

            //產品名稱關鍵字搜尋
            if (!string.IsNullOrEmpty(ProductName))
                Products = Products.Where(P => P.ProductName.Contains(ProductName));

            //價格區間搜尋
            if (MaxPrice != null && MinPrice != null)
                Products = Products.Where(P => P.Price >= MinPrice && P.Price <= MaxPrice);

            //產品描述關鍵字搜尋
            if (!string.IsNullOrEmpty(Description))
                Products = Products.Where(P => !string.IsNullOrEmpty(P.Description) && P.Description.Contains(Description));

            if (!Products.Any())
                return NotFound("沒有符合條件的商品");
            else
                return await Products.Select(P => GetProductDTO(P)).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CateID"></param>
        /// <param name="ProductName"></param>
        /// <param name="MaxPrice"></param>
        /// <param name="MinPrice"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        [HttpGet("FromSQL")]//界接口，不可重複
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsFromSQL(string? CateID, string? ProductName, decimal? MaxPrice, decimal? MinPrice, string? Description)
        {
            var Products = _context.Product.Include(C => C.Cate).OrderBy(P => P.Price).AsQueryable();

            //產品類別搜尋
            if (!string.IsNullOrEmpty(CateID))
                Products = Products.Where(P => P.CateID == CateID);

            //產品名稱關鍵字搜尋
            if (!string.IsNullOrEmpty(ProductName))
                Products = Products.Where(P => P.ProductName.Contains(ProductName));

            //價格區間搜尋
            if (MaxPrice != null && MinPrice != null)
                Products = Products.Where(P => P.Price >= MinPrice && P.Price <= MaxPrice);

            //產品描述關鍵字搜尋
            if (!string.IsNullOrEmpty(Description))
                Products = Products.Where(P => !string.IsNullOrEmpty(P.Description) && P.Description.Contains(Description));

            if (!Products.Any())
                return NotFound("沒有符合條件的商品");
            else
                return await Products.Select(P => GetProductDTO(P)).ToListAsync();
        }

        // GET: api/Products/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(string id)
        {
            var product = await _context.Product
                .Include(c => c.Cate)
                .Where(p => p.ProductID == id)
                .OrderBy(p => p.Price)
                .Select(p => GetProductDTO(p))
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(string id, Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Product.Add(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = product.ProductID }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(string id)
        {
            return _context.Product.Any(e => e.ProductID == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="P"></param>
        /// <returns></returns>
        static ProductDTO GetProductDTO(Product P)
        {
            return new ProductDTO
            {
                ProductID = P.ProductID,
                ProductName = P.ProductName,
                Price = P.Price,
                Description = P.Description,
                Picture = P.Picture,
                CateID = P.CateID,
                CateName = P.Cate.CateName
            };
        }
    }
}
