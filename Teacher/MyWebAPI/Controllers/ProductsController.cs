using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.DTOs;
using MyWebAPI.Models;

namespace MyWebAPI.Controllers
{
    //3.2.4 修改API介接路由為「api[controller]」
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
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProduct(decimal price = 0)
        {
            //4.1.2 使用Include()同時取得關聯資料
            //4.1.3 使用Where()改變查詢的條件並測試
            //4.1.4 使用OrderBy()相關排序方法改變資料排序並測試

            //var products = await _context.Product.Include(c => c.Cate).Where(p=>p.Price>= price)
            //    .OrderBy(p=>p.Price).ToListAsync();

            //4.1.5 使用Select()抓取特定的欄位並測試(這樣做無法return)
            //var products = await _context.Product.Include(c => c.Cate).Where(p => p.Price >= price)
            //    .OrderBy(p => p.Price).Select(p => new { 
            //        p.ProductID,
            //        p.ProductName,
            //        p.Price,
            //        p.Description,
            //        p.Picture,
            //        p.CreatedDate,
            //        p.CateID,
            //        p.Cate.CateName 
            //    }).ToListAsync();

            //4.2.3 改寫ProductsController裡的Get Action
            //var products = await _context.Product.Include(c => c.Cate).Where(p => p.Price >= price)
            // .OrderBy(p => p.Price).Select(p => new ProductDTO
            // {
            //     ProductID = p.ProductID,
            //     ProductName = p.ProductName,
            //     Price = p.Price,
            //     Description = p.Description,
            //     Picture = p.Picture,
            //     CateID = p.CateID,
            //     CateName = p.Cate.CateName
            // }).ToListAsync();


            //4.4.1 將資料轉換的程式寫成函數並再次改寫Get Action(※這種寫法架構才會好※)
            var products = await _context.Product.Include(c => c.Cate).Where(p => p.Price >= price)
             .OrderBy(p => p.Price).Select(p=>ItemProduct(p)).ToListAsync();


            return products;
        }


        //4.3.1 先使用Swagger測試及觀查目前Product的資料取得狀況(理解參數及介接口)
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(string id)
        {
            //4.4.1 將資料轉換的程式寫成函數並再次改寫Get Action(※這種寫法架構才會好※)
            //4.3.2 使用Include()同時取得關聯資料並使用ProductDTO來傳遞資料
            var product = await _context.Product.Include(c => c.Cate).Where(p => p.ProductID == id)
              .OrderBy(p => p.Price).Select(p => ItemProduct(p)).FirstOrDefaultAsync();


            if (product == null)
            {
                return NotFound("找不到產品資料");
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



        //4.4.1 將資料轉換的程式寫成函數並再次改寫Get Action(※這種寫法架構才會好※)
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
