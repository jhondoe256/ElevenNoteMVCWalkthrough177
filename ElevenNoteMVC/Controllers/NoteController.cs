using ElevenNote.Models.Categories;
using ElevenNote.Models.Notes;
using ElevenNote.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElevenNoteMVC.Controllers
{
    public class NoteController : Controller
    {
        private INoteService _noteService;
        private ICategoryServices _categoryServices;

        public NoteController(INoteService noteService, ICategoryServices categoryServices)
        {
            _noteService = noteService;
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var notes = await _noteService.GetNotesAsync();
            return View(notes);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var note = await _noteService.GetNoteAsync(id);
            if (note == null) return BadRequest();
            
            return View(note);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            IEnumerable<SelectListItem> categoryOptions =
                _categoryServices.GetCategorysAsync().GetAwaiter().GetResult().Select(c => new SelectListItem
                {
                    Text = c.CategoryTitle,
                    Value = c.Id.ToString()
                });

            NoteCreate noteCreate = new NoteCreate();
            noteCreate.CategoryOptions = categoryOptions;
            
            return View(noteCreate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoteCreate model)
        {
            //if (!ModelState.IsValid) return View(ModelState);

            if (await _noteService.AddNoteAsync(model))
                return RedirectToAction(nameof(Index),"Home");

            IEnumerable<SelectListItem> categoryOptions =
               _categoryServices.GetCategorysAsync().GetAwaiter().GetResult().Select(c => new SelectListItem
               {
                   Text = c.CategoryTitle,
                   Value = c.Id.ToString()
               });

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var note = await _noteService.GetNoteAsync(id);
            if (note == null) return NotFound();

            IEnumerable<SelectListItem> categoryOptions =
                _categoryServices.GetCategorysAsync().GetAwaiter().GetResult().Select(c => new SelectListItem
                {
                    Text = c.CategoryTitle,
                    Value = c.Id.ToString()
                });

            NoteEdit noteEdit = new NoteEdit() 
            {
                Title = note.Title,
                Id = note.Id,
                CategoryOptions = categoryOptions,
                NoteContent = note.NoteContent,
                CategoryEntityId = note.CategoryEntity.Id
            };
            

            return View(noteEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NoteEdit model)
        {
            if (!ModelState.IsValid) return View(ModelState);

            if (await _noteService.EditNoteAsync(model))
                return RedirectToAction(nameof(Index), "Home");


            IEnumerable<SelectListItem> categoryOptions =
               _categoryServices.GetCategorysAsync().GetAwaiter().GetResult().Select(c => new SelectListItem
               {
                   Text = c.CategoryTitle,
                   Value = c.Id.ToString()
               });

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var note = await _noteService.GetNoteAsync(id);
            if (note == null) return BadRequest();

            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            if (await _noteService.DeleteNoteAsync(id)) return RedirectToAction(nameof(Index), "Home");


            return StatusCode(500,"Internal Server Error.");
        }
    }
}
