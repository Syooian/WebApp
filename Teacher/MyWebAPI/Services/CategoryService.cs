using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.DTOs;
using MyWebAPI.Models;

namespace MyWebAPI.Services
{
    public class CategoryService
    {
        private readonly GoodStoreContext _context;

        public CategoryService(GoodStoreContext context)
        {
            _context = context;
        }
        //8.2.1 在Service資料夾中建立CategoryService，並將CategoriesController裡的兩個Get Action相關的商業邏輯移至此撰寫
        //      (包括ItemProduct()方法一併移入CategoryService)

        public async Task<List<CategoryDTO>> GetCategory()
        {
          
            return await _context.Category.Include(c => c.Product).Select(c => ItemCategory(c)).ToListAsync();
        }


        public async Task<CategoryDTO> GetCategory(string id)
        {
            
            var category = await _context.Category.Include(c => c.Product).Where(c => c.CateID == id)
                .Select(c => ItemCategory(c)).FirstOrDefaultAsync();

      
            return category;
        }

        private static CategoryDTO ItemCategory(Category c)
        {
            var result = new CategoryDTO()
            {
                CateID = c.CateID,
                CateName = c.CateName,
                //ProductCount = c.Product.Count(),
                Product = c.Product
            };

            return result;

        }

        public async Task<Category> getCategory(string id)
        {
            return await _context.Category.FindAsync(id);
        }

        public async Task<Category> UpdateCategory(Category cate, [FromForm] CategoryPutDTO category)
        {
            
            cate.CateName = category.CateName;

            _context.Entry(cate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }



            return cate;
        }

        public async Task<Category> InsertCategory(CategoryPostDTO category)
        {
          
            Category cate = new Category()
            {
                CateID = category.CateID,
                CateName = category.CateName
            };

            _context.Category.Add(cate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                
                 throw;
                
            }

            return cate;
        }

        public async Task<bool> DeleteCategory(Category cate)
        {
         

            _context.Category.Remove(cate);
 

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {

                return false;

            }

            return true;
        }
    }
}
