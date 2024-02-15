using ElevenNote.Models.Categories;
using ElevenNote.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ElevenNoteMVC.Controllers
{
    public class CategoryController : Controller
    {

        private ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryServices.GetCategorysAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var category = await _categoryServices.GetCategoryAsync(id);
            if (category == null) return RedirectToAction(nameof(MyNotFound));
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreate model)
        {
            if (!ModelState.IsValid)  return View(ModelState);

            if(await _categoryServices.AddCategoryAsync(model))
                return RedirectToAction(nameof(Index));
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryServices.GetCategoryAsync(id);
            if (category == null) return RedirectToAction(nameof(MyNotFound));

            var categoryEdit = new CategoryEdit
            {
                Id= category.Id,
                CategoryTitle = category.CategoryTitle
            };
            
            return View(categoryEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryEdit model)
        {
            if (!ModelState.IsValid) return View(ModelState);

            if (await _categoryServices.EditCategoryAsync(model))
                return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var category = await _categoryServices.GetCategoryAsync(id.Value);
            if (category == null) return RedirectToAction(nameof(MyNotFound));
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _categoryServices.DeleteCategoryAsync(id))
                return RedirectToAction(nameof(Index));

            return StatusCode(500,"Internal Server Error.");
        }


        //custom view for an error/optional.....
        public async Task<IActionResult> MyNotFound()
        {
            return View();
        }
    }
}
