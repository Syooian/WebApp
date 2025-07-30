using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
            var SQLQuery = $"select P.ProductID, P.ProductName, P.Price, P.Description, P.Picture, P.CateID, C.CateName from Product as P inner join Category as C on C.CateID = P.CateID where 1=1 ";
            //加入where 1=1，讓此判斷式在所有情況下都成立，便於後續添加其他條件
            /*
             * SQL Injection攻擊：
             * ' or 1=1'--
             */

            var SQLPara = new List<SqlParameter>();

            //產品類別搜尋
            if (!string.IsNullOrEmpty(CateID))
            {
                //SQLQuery += $"and P.CateID = '{CateID}' ";
                SQLQuery += $"and P.CateID = @cate ";
                SQLPara.Add(new SqlParameter("@cate", CateID));
            }

            //產品名稱關鍵字搜尋
            if (!string.IsNullOrEmpty(ProductName))
            {
                //SQLQuery += $"and P.ProductName like '%{ProductName}%' ";
                SQLQuery += $"and P.ProductName like @productName ";
                SQLPara.Add(new SqlParameter("@productName", $"%{ProductName}%"));
            }

            //價格區間搜尋
            if (MaxPrice != null && MinPrice != null)
            {
                //SQLQuery += $"and P.Price between {MinPrice} and {MaxPrice} ";
                SQLQuery += $"and P.Price between @minPrice and @maxPrice ";
                SQLPara.Add(new SqlParameter("@minPrice", MinPrice));
                SQLPara.Add(new SqlParameter("@maxPrice", MaxPrice));
            }

            //產品描述關鍵字搜尋
            if (!string.IsNullOrEmpty(Description))
            {
                //SQLQuery += $"and P.Description like '%{Description}%' ";
                SQLQuery += $"and P.Description like @description ";
                SQLPara.Add(new SqlParameter("@description", $"%{Description}%"));
            }

            var Products = await _context.ProductDTO.FromSqlRaw(SQLQuery, SQLPara.ToArray()).ToListAsync();

            if (!Products.Any())
                return NotFound("沒有符合條件的商品");
            else
                return Products;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CateID"></param>
        /// <returns></returns>
        [HttpGet("fromProc/{CateID}")]//Path
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsFromProc(string CateID)
        {
            //4.8.4 使用預存程序進行查詢(參數的傳遞請使用SqlParameter)
            //string SQL = $"exec getProductWithCateName '{CateID}'";//會發生SQL Injection錯誤
            string SQL = $"exec getProductWithCateName @cateID";
            var cateID = new SqlParameter("@cateID", CateID);

            var Products = await _context.ProductDTO.FromSqlRaw(SQL, cateID).ToListAsync();

            if (Products == null || Products.Count == 0)
                return NotFound("找不到產品資料");
            return Products;
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
