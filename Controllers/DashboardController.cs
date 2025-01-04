using Forum.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
