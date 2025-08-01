using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITest.Data;
using WebAPITest.DTOs;
using WebAPITest.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPITest.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly GoodStoreContext _context;

        public CategoriesController(GoodStoreContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CateID"></param>
        /// <param name="CateName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategorys(string? CateID, string? CateName)
        {
            var Categories = _context.Category.Include(P => P.Product).OrderBy(C => C.CateID).AsQueryable();

            if (!string.IsNullOrEmpty(CateID))
                Categories = Categories.Where(C => C.CateID == CateID);

            if (!string.IsNullOrEmpty(CateName))
                Categories = Categories.Where(C => C.CateName.Contains(CateName));

            if (!Categories.Any())
                return NotFound("沒有符合條件的Category");
            else
                return await Categories.Select(C => GetCategoryDTO(C)).ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(string id)
        {
            var category = await _context.Category.Include(C => C.Product).Where(C => C.CateID == id).Select(C => GetCategoryDTO(C)).FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(string id, Category category)
        {
            if (id != category.CateID)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPut("PutCategoryDTO/{id}")]
        public async Task<IActionResult> PutCategory(string id, CategoryPutDTO category)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var Cate = await _context.Category.FindAsync(id);
            //if (Cate == null)
            //{
            //    return NotFound("查無資料");
            //}

            Cate.CateName = category.CateName;

            _context.Entry(Cate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return NoContent();//代表成功但沒有內容返回 (204)
            return Ok(Cate);//代表成功並返回內容 (200)
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Category.Add(category);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CategoryExists(category.CateID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCategory", new { id = category.CateID }, category);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CateID"></param>
        /// <param name="CateName"></param>
        /// <returns></returns>
        [HttpPost("FromPostDTO")]
        //public async Task<ActionResult<CategoryPostDTO>> PostCategory(string CateID, string CateName)
        public async Task<ActionResult<CategoryPostDTO>> PostCategory(CategoryPostDTO CategoryPostDTO)
        {
            //CategoryPostDTO DTO = new CategoryPostDTO()
            //{
            //    CateID = CateID,
            //    CateName = CateName
            //};
            //var Category = new Category
            //{
            //    CateID = DTO.CateID,
            //    CateName = DTO.CateName
            //};

            var Category = new Category
            {
                CateID = CategoryPostDTO.CateID,
                CateName = CategoryPostDTO.CateName
            };

            _context.Category.Add(Category);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CategoryExists(Category.CateID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCategory", new { id = Category.CateID }, Category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(string id)
        {
            return _context.Category.Any(e => e.CateID == id);
        }

        static CategoryDTO GetCategoryDTO(Category C)
        {
            return new CategoryDTO
            {
                CateID = C.CateID,
                CateName = C.CateName,
                //ProductCount = C.Product.Count,//可在Controller內return數量，但是以MVC的習慣和設計邏輯來說，商業邏輯應該要寫在Model上
                Products = C.Product
            };
        }
    }
}
