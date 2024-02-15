using ElevenNote.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services.Contracts
{
    public interface ICategoryServices
    {
        Task<bool> AddCategoryAsync(CategoryCreate model);
        Task<bool> EditCategoryAsync(CategoryEdit model);
        Task<bool> DeleteCategoryAsync(int id);
        Task<CategoryDetail> GetCategoryAsync(int id);
        Task<List<CategoryListItem>> GetCategorysAsync();
    }
}
