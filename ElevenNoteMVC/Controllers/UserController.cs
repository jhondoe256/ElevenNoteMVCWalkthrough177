using ElevenNote.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ElevenNoteMVC.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
