using AutoMapper;
using ElevenNote.Data.Data;
using ElevenNote.Models.Categories;
using ElevenNote.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services.Categories
{
    public class MappedCategoryService : ICategoryServices
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public MappedCategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddCategoryAsync(CategoryCreate model)
        {
            //var category = _mapper.Map<CategoryEntity>(model);
            await _context.CategoryEntities.AddAsync(_mapper.Map<CategoryEntity>(model));
            await _context.SaveChangesAsync();
            return true;
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
            var category = await 
                _context
                .CategoryEntities
                .AsNoTracking()
                .FirstOrDefaultAsync(x=> x.Id == model.Id);
            
            if (category == null) return false;

            var conversion = _mapper.Map(model,category);
            _context.CategoryEntities.Update(conversion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CategoryDetail> GetCategoryAsync(int id)
        {
            return  _mapper.Map<CategoryDetail>(await _context.CategoryEntities.FindAsync(id));
        }

        public async Task<List<CategoryListItem>> GetCategorysAsync()
        {
            return _mapper.Map<List<CategoryListItem>>(await _context.CategoryEntities.ToListAsync());
        }
    }
}
