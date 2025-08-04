using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.DTOs;
using MyWebAPI.Models;
using MyWebAPI.Services;

namespace MyWebAPI.Controllers
{
    //3.2.4 修改API介接路由為「api[controller]」
    [Route("api[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //private readonly GoodStoreContext _context;

        //public ProductsController(GoodStoreContext context)
        //{
        //    _context = context;
        //}


        //4.7.8 修改ProductsController上方所注入的GoodStoreContext為GoodStoreContext2
        private readonly GoodStoreContextG2 _context;
        private readonly SomeService _service;

        public ProductsController(GoodStoreContextG2 context, SomeService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Products
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProduct(string? cateID, string? productName, decimal? minPrice, decimal? maxPrice, string? description)
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
            //var products = await _context.Product.Include(c => c.Cate).Where(p => p.Price >= price)
            //    .OrderBy(p => p.Price).Select(p => ItemProduct(p)).ToListAsync();


            //var products = await _context.Product.Include(c => c.Cate).OrderBy(p => p.Price).Select(p => ItemProduct(p)).ToListAsync();

            //4.4.8 修改先將資料載入內存的寫法
            var products = _context.Product.Include(c => c.Cate).OrderBy(p => p.Price).AsQueryable();

            //4.4.2 加入產品類別搜尋
            if (!string.IsNullOrEmpty(cateID))
            {
                //products = products.Where(p => p.CateID == cateID).ToList();
                products = products.Where(p => p.CateID == cateID);

            }
            //4.4.3 加入產品名稱關鍵字搜尋
            if (!string.IsNullOrEmpty(productName))
            {
                //products = products.Where(p => p.ProductName.Contains(productName)).ToList();
                products = products.Where(p => p.ProductName.Contains(productName));
            }
            //4.4.4 加入價格區間搜區
            if (minPrice.HasValue && maxPrice.HasValue)
            {
                //products = products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
                products = products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
            }


            //4.4.5 加入產品敘述關鍵字搜尋
            if (!string.IsNullOrEmpty(description))
            {
                //products = products.Where(p => p.Description.Contains(description)).ToList();
                products = products.Where(p => p.Description.Contains(description));
            }


            if (products == null || products.Count() == 0)
            {
                return NotFound("找不到產品資料");
            }


            return await products.Select(p => ItemProduct(p)).ToListAsync();
        }

        //4.6.1 新增一個Get Action GetProductFromSQL()並設定介接口為[HttpGet("fromSQL")]
        [HttpGet("fromSQL")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductFromSQL(string? cateID, string? productName, decimal? minPrice, decimal? maxPrice, string? description)
        {
            //4.6.2 用SQL語法撰寫與先前一樣的功能並使用DTO傳遞結果
            //var products = _context.Product.Include(c => c.Cate).OrderBy(p => p.Price).AsQueryable();


            string sql = "select p.ProductID, p.ProductName, p.Price, p.Description, p.Picture, p.CateID, c.CateName " +
                " from Product as p inner join Category as c on p.CateID=c.CateID where 1=1 ";

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(cateID))
            {
                //sql += $" and p.CateID = '{cateID}' ";
                sql += " and p.CateID = @cate ";
                parameters.Add(new SqlParameter("@cate", cateID));
            }

            if (!string.IsNullOrEmpty(productName))
            {
                //sql += $" and p.ProductName like '%{productName}%' ";
                sql += " and p.ProductName like @productName ";
                parameters.Add(new SqlParameter("@productName", $"%{productName}%"));
            }

            if (minPrice.HasValue && maxPrice.HasValue)
            {
                //sql += $" and between {minPrice} and {maxPrice} ";
                sql += " and p.Price between @minPrice and @maxPrice ";
                parameters.Add(new SqlParameter("@minPrice", minPrice));
                parameters.Add(new SqlParameter("@maxPrice", maxPrice));
            }


            if (!string.IsNullOrEmpty(description))
            {
                //sql += $" and p.Description like '%{description}%' ";
                sql += $" and p.Description like @description ";
                parameters.Add(new SqlParameter("@description", $"%{description}%"));
            }

            //4.6.4 使用Swagger測試(這裡會發生錯誤，因為使用了合併查詢)
            //var products = await _context.Product.FromSqlRaw(sql).ToListAsync();

            //4.6.6 將_context.Product.FromSqlRaw(sql).ToListAsync();改為_context.ProductDTO.FromSqlRaw(sql).ToListAsync();
            var products = await _context.ProductDTO.FromSqlRaw(sql, parameters.ToArray()).ToListAsync();

            if (products == null || products.Count() == 0)
            {
                return NotFound("找不到產品資料");
            }


            return products;
        }

        //4.8.2 在ProductsController中建立一個新的Get Action
        //4.8.3 設置介接口為[HttpGet("fromProc/{id}")]，Action名稱可自訂，並使用ProductDTO來傳遞資料
        [HttpGet("fromProc/{id}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductFromProc(string id)
        {
            //4.8.4 使用預存程序進行查詢(參數的傳遞請使用SqlParameter)
            string sql = $"exec getProductWithCateName @cateID";

            var cateID = new SqlParameter("@cateID", id);

            var products = await _context.ProductDTO.FromSqlRaw(sql, cateID).ToListAsync();

            if (products == null || products.Count() == 0)
            {
                return NotFound("找不到產品資料");
            }


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


        //6.1.7 改寫ProductsController中Put Action內容
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(string id, [FromForm] ProductPutDTO product)
        {
            //if (id != product.ProductID)
            //{
            //    return BadRequest();
            //}
            if (id == null)
            {
                return BadRequest();
            }

            var p = await _context.Product.FindAsync(id);
            if (p == null)
            {
                return NotFound("查無資料");
            }

            //檢查是否有新照片上傳
            if (product.Picture != null && product.Picture.Length != 0)
            {
                FileUpload(product.Picture, id);

            }

            p.ProductName = product.ProductName;
            p.Price = product.Price;
            p.Description = product.Description;


            _context.Entry(p).State = EntityState.Modified;

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

            return Ok(p);
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

        //5.2.4 建立一個新的Post Action，介接口設定為[HttpPost("PostWithPhoto")]，並加入上傳檔案的動作
        [HttpPost("PostWithPhoto")]
        public async Task<ActionResult<ProductPostDTO>> PostProductWithPhoto([FromForm] ProductPostDTO product)
        {
            //判斷檔案是否上傳
            if (product.Picture == null || product.Picture.Length == 0)
            {
                return BadRequest("未上傳商品圖片");
            }

            string fileName = await FileUpload(product.Picture, product.ProductID);

            if (fileName == "")
            {
                return BadRequest("上傳的檔案格式不正確，請上傳jpg、jpeg或png格式的圖片");
            }


            Product p = new Product
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                Picture = fileName,
                CateID = product.CateID
            };



            //寫入資料庫
            _context.Product.Add(p);
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

        //7.1.1 改寫ProductsController中Delete Action內容，加入刪除照片的功能
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            //刪除商品照片
            if(! await FileDelete(product.Picture))
            {
                return BadRequest("刪除商品照片失敗，請檢查檔案是否存在或權限問題。");
            }


            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //7.1.5 建立可刪除多筆資料的Delete Action(批次刪除)，介接口設為[HttpDelete("ByCatID")]，方法名稱可自訂，傳入的參為為商品類別ID
        //方法名稱可自訂，傳入的參為為商品類別ID
        [HttpDelete("ByCateID")]
        public async Task<IActionResult> DeleteProductsByCateID(string cateID)
        {
            var products = await _context.Product.Where(p => p.CateID == cateID).ToListAsync();

            if (products == null)
            {
                return NotFound();
            }


            foreach(var p in products)
            {
                //刪除商品照片
                if (!await FileDelete(p.Picture))
                {
                    return BadRequest("刪除商品照片失敗，請檢查檔案是否存在或權限問題。");
                }
                //刪除商品資料
                _context.Product.Remove(p);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return NotFound("刪除商品失敗，請檢查商品是否存在。");
            }

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

        //5.2.5 將上傳檔案寫成一個獨立的方法
        private async Task<string> FileUpload(IFormFile Photo, string PID)
        {
            //判斷上傳的檔案是否為圖片格式
            var extension = Path.GetExtension(Photo.FileName).ToLower();
            var allowedExtension = new[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtension.Contains(extension))
            {
                return "";
            }


            //檔案上傳的路徑
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductPhotos");

            //確保目錄存在
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            //檔案名稱(ProductID+副檔名)
            var fileName = PID + Path.GetExtension(Photo.FileName);

            var filePath = Path.Combine(uploadPath, fileName); //"/wwwroot/ProductPhotos/XXXXX.jpg";

            //儲存檔案
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Photo.CopyToAsync(stream);
            }


            return fileName; //回傳檔案名稱
        }


        //7.1.2 將刪除照片功能另建立FileDelete()方法
        private async Task<bool> FileDelete(string fileName)
        {

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductPhotos");

            var filePath = Path.Combine(path, fileName);

            if(System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);

                    return true;
                }
                catch (Exception ex)
                {
                  
                  
                    return false;
                }
            }


          

            return false;
        }
    }
}