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
using MyWebAPI.QueryParameters;
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
        private readonly ProductService _productService;

        public ProductsController(GoodStoreContextG2 context, ProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        //8.3.5 改寫ProductsController裡的Get Action寫法，僅留下控制邏輯
        //8.3.8 改寫參數傳入的方式為傳入ProductParam類別(全部相關的地方都要改寫)
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProduct([FromQuery]ProductParam productParam)
        {

            var products = await _productService.GetProduct(productParam);


            if (products == null || products.Count() == 0)
            {
                return NotFound("找不到產品資料");
            }


            return products;
        }

        //8.3.5 改寫ProductsController裡的Get Action寫法，僅留下控制邏輯
        //8.3.8 改寫參數傳入的方式為傳入ProductParam類別(全部相關的地方都要改寫)
        [HttpGet("fromSQL")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductFromSQL([FromQuery] ProductParam productParam)
        {

            var products = await  _productService.GetProductFromSQL(productParam);

            if (products == null || products.Count()==0)
            {
                return NotFound("找不到產品資料");
            }


            return products;
        }


        //8.3.5 改寫ProductsController裡的Get Action寫法，僅留下控制邏輯
        [HttpGet("fromProc/{id}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductFromProc(string id)
        {

            var result = await _productService.getProductWithCateName(id);

            if (result == null || result.Count() <= 0)
            {
                return NotFound("找不到資料");
            }

            return result;
        }


        //8.3.5 改寫ProductsController裡的Get Action寫法，僅留下控制邏輯
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(string id)
        {
            var product = await _productService.GetProduct(id);

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
        //[HttpPost]
        //public async Task<ActionResult<Product>> PostProduct(Product product)
        //{
        //    _context.Product.Add(product);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (ProductExists(product.ProductID))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetProduct", new { id = product.ProductID }, product);
        //}

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
            if (!await FileDelete(product.Picture))
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


            foreach (var p in products)
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