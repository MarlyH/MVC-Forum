using Forum.Data;
using Forum.Interfaces;
using Forum.Models;
using Forum.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Thread = Forum.Models.Thread;

namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumRepository _forumRepository;
        private readonly UserManager<User> _userManager;

        public ForumController(IForumRepository forumRepository, UserManager<User> userManager)
        {
            _forumRepository = forumRepository;
            _userManager = userManager;
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
        [Authorize]
        public async Task<IActionResult> CreateThread(int groupId=1) // default group value in case user manually navigates to page
        {
            var group = await _forumRepository.GetGroupByIdAsync(groupId);

            if (group == null) return NotFound();

            var registerVM = new CreateThreadViewModel
            {
                PassedGroupId = groupId,
                Groups = await _forumRepository.GetAllGroupsAsync()
            };
            return View(registerVM);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateThread(CreateThreadViewModel createThreadVM)
        {
            if (!ModelState.IsValid)
            {
                // Reload groups to ensure the view model has the necessary data for rendering the form correctly
                createThreadVM.Groups = await _forumRepository.GetAllGroupsAsync();
                return View(createThreadVM);
            }

            // get the group and user objects needed for building out a new Thread object
            var group = await _forumRepository.GetGroupByIdAsync(createThreadVM.GroupId);
            var user = await _userManager.GetUserAsync(User);

            if (user == null || group == null) return NotFound();

            // build out new Thread object
            var newThread = new Thread()
            {
                Title = createThreadVM.Title,
                Content = createThreadVM.Content,
                AuthorId = user.Id,
                Author = user,
                Group = group,
                DateCreated = DateTime.Now
            };

            await _forumRepository.AddThreadAsync(newThread);

            // redirect to view the newly-created thread
            return RedirectToAction("ViewThread", "Forum", new { threadId = newThread.Id });         
        }
        public async Task<IActionResult> ViewThread(int threadId)
        {
            var thread = await _forumRepository.GetThreadByIdAsync(threadId);
            var user = await _userManager.GetUserAsync(User);

            if (thread == null) return NotFound();

            var viewThreadVM = new ViewThreadViewModel()
            {
                Thread = thread,
                SignedInUserId = user?.Id 
                
            };

            return View(viewThreadVM);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReply(ViewThreadViewModel viewThreadVM)
        {
            var thread = await _forumRepository.GetThreadByIdAsync(viewThreadVM.ThreadId);
            var author = await _userManager.GetUserAsync(User);
            if (author == null || thread == null) return NotFound();

            if (!ModelState.IsValid) 
            {
                // Reload thread to ensure the view model has the necessary data for rendering the form correctly
                viewThreadVM.Thread = thread;
                return View("viewThread", viewThreadVM); 
            }

            var newReply = new ThreadReply()
            {
                Content = viewThreadVM.ReplyContent,
                DateCreated = DateTime.Now,
                Thread = thread,
                ThreadId = thread.Id,
                Author = author,
                AuthorId = author.Id,
            };  

            await _forumRepository.AddReplyAsync(newReply);

            return RedirectToAction("ViewThread", "Forum", new { threadId = newReply.ThreadId });
        }
        [Authorize]
        public async Task<IActionResult> EditThread(int threadId) 
        {
            var thread = await _forumRepository.GetThreadByIdAsync(threadId);
            var editThreadVM = new EditThreadViewModel()
            {
                Thread = thread,
                Content = thread.Content,
            };
            return View(editThreadVM);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditThread(EditThreadViewModel editThreadVM) // TODO: site breaks when editing thread to be empty
        {
            if (!ModelState.IsValid) return NotFound();

            var author = await _userManager.FindByIdAsync(editThreadVM.AuthorId);
            var group = await _forumRepository.GetGroupByIdAsync(editThreadVM.GroupId);
            var thread = await _forumRepository.GetThreadByIdAsync(editThreadVM.ThreadId);

            if (author == null || group == null || thread == null) return NotFound();

            thread.Title = editThreadVM.Title;
            thread.Content = editThreadVM.Content;
            thread.DateLastEdited = DateTime.Now;
            thread.AuthorId = editThreadVM.AuthorId;
            thread.Author = author;
            thread.GroupId = editThreadVM.GroupId;
            thread.Group = group;

            await _forumRepository.UpdateThreadAsync(thread);

            // redirect to the now updated thread
            return RedirectToAction("ViewThread", "Forum", new { threadId =  thread.Id });
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteThread(int threadId)
        {
            var thread = await _forumRepository.GetThreadByIdAsync(threadId);
            if (thread == null) return NotFound();

            await _forumRepository.DeleteThreadAsync(thread);

            return RedirectToAction("Index", "Forum"); // TODO: should probably alert the user their action was successful
        }
    }
}
