using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.DTOs;
using MyWebAPI.Models;
using MyWebAPI.Services;

namespace MyWebAPI.Controllers
{
    //3.1.4 修改API介接路由為「api[controller]」
    [Route("api[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly GoodStoreContext _context;
        //8.2.4 在CategoriesController裡注入CategoryService服務
        private readonly CategoryService _categoryService;

        public CategoriesController(GoodStoreContext context, CategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }

        //8.2.5 改寫CategoriesController裡的兩個Get Action寫法，僅留下控制邏輯
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategory()
        {
            var categories = await _categoryService.GetCategory();

            //控制邏輯：如果沒有找到任何資料，則回傳NotFound
            if (categories == null || !categories.Any())
            {
                return NotFound("沒有找到任何資料");
            }

            return categories;
        }

        //8.2.5 改寫CategoriesController裡的兩個Get Action寫法，僅留下控制邏輯
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(string id)
        {
           
            var category = await _categoryService.GetCategory(id);


            if (category == null)
            {
                return NotFound("沒有找到任何資料");
            }

            return category;
        }



        //8.2.7 將Post、Put及Delete重構
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(string id,[FromForm] CategoryPutDTO category)
        {
           
            if (id ==null)
            {
                return BadRequest();
            }

            var cateOld = await _categoryService.getCategory(id);

            if (cateOld == null)
            {
                return NotFound("查無資料");
            }

           var cate = await _categoryService.UpdateCategory(cateOld, category);

            return Ok(cate);
        }


        [HttpPost]
        public async Task<ActionResult<CategoryPostDTO>> PostCategory(CategoryPostDTO category)
        {

            if (CategoryExists(category.CateID))
            {
                return BadRequest();
            }

            var cate =_categoryService.InsertCategory(category);

            return Ok(cate);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var category = await _categoryService.getCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            var result = await _categoryService.DeleteCategory(category);

            if (!result)
            {
                return BadRequest();
            }


            return NoContent();
        }

        


        private bool CategoryExists(string id)
        {
            return _context.Category.Any(e => e.CateID == id);
        }


      
    }
}
