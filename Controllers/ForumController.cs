using Forum.Data;
using Forum.Interfaces;
using Forum.Models;
using Forum.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumRepository _forumRepository;

        public ForumController(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }
        public IActionResult Index()
        {
            var forumVM = new ForumViewModel
            {
                Categories = _forumRepository.GetAllCategoriesAsync().Result,
                //Groups = _forumRepository.GetAllGroupsAsync().Result,
            };

            return View(forumVM);
        }
        public async Task<IActionResult> ThreadCategory(int categoryId)
        {
            var category = await _forumRepository.GetCategoryByIdAsync(categoryId);

            var forumVM = new ForumViewModel
            {
                Category = category,
            };

            return View(forumVM);
        }
        public async Task<IActionResult> ThreadGroup(int categoryId, int groupId)
        {
            var group = await _forumRepository.GetGroupByIdAsync(groupId);

            // ensure the group actually exists under the specified category
            if (group == null || group.CategoryId != categoryId)
            {
                return NotFound();
            }

            var forumVM = new ForumViewModel
            {
                Group = group,
            };
            return View(forumVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(ThreadCategory category)
        {
            await _forumRepository.AddCategoryAsync(category);
            return RedirectToAction("Index");
        }
    }
}
