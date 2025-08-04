using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<IActionResult> PutProduct(string id, [FromForm] ProductPutDTO Product)
        {
            //if (id != product.ProductID)
            //{
            //    return BadRequest();
            //}
            if (id == null)
                return BadRequest();

            var P = await _context.Product.FindAsync(id);
            if (P == null)
            {
                return NotFound("查無資料");
            }

            //檢查是否有新照片上傳
            if (Product.Picture != null && Product.Picture.Length != 0)
            {
                FileUpload(Product.Picture, id);
            }

            P.ProductName = Product.ProductName;
            P.Price = Product.Price;
            P.Description = Product.Description;

            _context.Entry(P).State = EntityState.Modified;

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

            return Ok(P);
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)//沒標註就是FromBody
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="ProductName"></param>
        /// <param name="Price"></param>
        /// <param name="Description"></param>
        /// <param name="CateID"></param>
        /// <returns></returns>
        [HttpPost("fromQuery")]
        public async Task<ActionResult<Product>> PostProduct(/*[FromQuery]*/ string ProductID, string ProductName, decimal Price, string? Description, string CateID)
        {
            var Product = new Product()
            {
                ProductID = ProductID,
                ProductName = ProductName,
                Price = Price,
                Description = Description,
                Picture = ProductID,
                CateID = CateID
            };

            _context.Product.Add(Product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(Product.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = Product.ProductID }, Product);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductPostDTO"></param>
        /// <returns></returns>
        [HttpPost("PostWithPhoto")]
        public async Task<ActionResult<ProductPostDTO>> PostProductWithPhoto([FromForm] ProductPostDTO ProductPostDTO)
        {
            var Product = new Product()
            {
                ProductID = ProductPostDTO.ProductID,
                ProductName = ProductPostDTO.ProductName,
                Price = ProductPostDTO.Price,
                Description = ProductPostDTO.Description,
                CateID = ProductPostDTO.CateID
            };

            //上傳檔案
            UploadPhoto(Product, ProductPostDTO);

            _context.Product.Add(Product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(Product.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = Product.ProductID }, Product);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="ProductName"></param>
        /// <param name="Price"></param>
        /// <param name="Description"></param>
        /// <param name="CateID"></param>
        /// <returns></returns>
        [HttpPost("fromForm")]
        public async Task<ActionResult<Product>> PostProductFromForm([FromForm] string ProductID, string ProductName, decimal Price, string? Description, string CateID)
        {
            var Product = new Product()
            {
                ProductID = ProductID,
                ProductName = ProductName,
                Price = Price,
                Description = Description,
                Picture = ProductID,
                CateID = CateID
            };

            _context.Product.Add(Product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(Product.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = Product.ProductID }, Product);
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

            //刪除商品照片
            if (!await FileDelete(product.Picture))
            {
                return BadRequest("刪除商品照片失敗，請檢查檔案是否存在或權限問題。");
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CateID"></param>
        /// <returns></returns>
        [HttpDelete("ByCateID")]
        public async Task<IActionResult> DeleteProductsByCateID(string CateID)
        {
            var Products = await _context.Product.Where(P => P.CateID == CateID).ToListAsync();
            if (Products == null)
            {
                return NotFound();
            }

            //刪除商品照片
            foreach (var Product in Products)
            {
                if (!await FileDelete(Product.Picture))
                {
                    return BadRequest("刪除商品照片失敗，請檢查檔案是否存在或權限問題。");
                }

                //刪除商品資料
                _context.Product.Remove(Product);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"刪除商品失敗 : {ex.Message}");
                return BadRequest("刪除商品失敗");
            }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Product"></param>
        /// <param name="ProductPostDTO"></param>
        static void UploadPhoto(Product Product, ProductPostDTO ProductPostDTO)
        {
            if (ProductPostDTO.Picture != null || ProductPostDTO.Picture.Length == 0)
            {
                //檔案上傳路徑
                var UploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductPhotos");

                //檢查路徑
                if (!Directory.Exists(UploadPath))
                    Directory.CreateDirectory(UploadPath);

                //檔案名稱(ProductID.jpg)
                var FileName = ProductPostDTO.ProductID + Path.GetExtension(ProductPostDTO.Picture.FileName);
                var FilePath = Path.Combine(UploadPath, FileName);

                //上傳
                using (var Stream = new FileStream(FilePath, FileMode.Create))
                {
                    ProductPostDTO.Picture.CopyTo(Stream);
                }

                Product.Picture = FileName;
            }
        }

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

            if (System.IO.File.Exists(filePath))
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
