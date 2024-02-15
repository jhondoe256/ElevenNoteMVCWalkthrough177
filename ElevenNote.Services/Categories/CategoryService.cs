using ElevenNote.Data.Data;
using ElevenNote.Models.Categories;
using ElevenNote.Models.Notes;
using ElevenNote.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services.Categories
{
    public class CategoryService : ICategoryServices
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCategoryAsync(CategoryCreate model)
        {
            try 
            {
                var entity = new CategoryEntity
                {
                    CategoryTitle = model.CategoryTitle,
                    CreatedUtc = DateTimeOffset.UtcNow
                };

                await _context.CategoryEntities.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
            }
            return false;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.CategoryEntities.FindAsync(id);
            if (category == null) return false;

            _context.CategoryEntities.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditCategoryAsync(CategoryEdit model)
        {
            try 
            {
                var category = await _context.CategoryEntities.FindAsync(model.Id);
                if (category == null) return false;

                category.CategoryTitle = model.CategoryTitle;
                category.ModifiedUtc = DateTimeOffset.UtcNow;
                
                await _context.SaveChangesAsync();
                return true;
            }
            catch 
            {
            }
            return false;
        }

        public async Task<CategoryDetail> GetCategoryAsync(int id)
        {
            var category =
                         await 
                        _context
                        .CategoryEntities.Include(c=>c.Notes)
                        .SingleOrDefaultAsync(c=>c.Id == id);
            
            if (category == null) return null;

            return new CategoryDetail
            {
                Id = category.Id,
                CategoryTitle= category.CategoryTitle,
                CreatedUtc = category.CreatedUtc,
                ModifiedUtc = category.ModifiedUtc,
                Notes = category.Notes.Select(cn=>new NoteListItem 
                {
                    Id = cn.Id,
                    Title= cn.Title,
                    CreatedUtc=cn.CreatedUtc,
                }).ToList()
            };
        }

        public async Task<List<CategoryListItem>> GetCategorysAsync()
        {
           return await _context.CategoryEntities.Select(c=>new CategoryListItem 
           {
               Id = c.Id,
               CategoryTitle = c.CategoryTitle,
               CreatedUtc = c.CreatedUtc
           }).ToListAsync();
        }
    }
}
