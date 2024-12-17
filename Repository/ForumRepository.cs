using Forum.Data;
using Forum.Interfaces;
using Forum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Forum.Repository
{
    public class ForumRepository : IForumRepository
    {
        private readonly ApplicationDbContext _context;

        public ForumRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task<bool> AddCategoryAsync(ThreadCategory category)
        {
            await _context.AddAsync(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddGroupAsync(ThreadGroup group)
        {
            await _context.AddAsync(group);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<bool> AddReplyAsync(ThreadReply reply)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddThreadAsync(Models.Thread thread)
        {
            throw new NotImplementedException();
        }

        // Read
        public async Task<IEnumerable<ThreadCategory>> GetAllCategoriesAsync()
        {
            return await _context.ThreadCategories
                .Include(c => c.Groups)
                .ToListAsync();
        }

        public async Task<IEnumerable<ThreadGroup>> GetAllGroupsAsync()
        {
            return await _context.ThreadGroups.ToListAsync();
        }

        public Task<IEnumerable<Models.Thread>> GetAllThreadsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ThreadCategory> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.ThreadCategories
                .Include(c => c.Groups)
                .FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<ThreadGroup> GetGroupByIdAsync(int groupId)
        {
            return await _context.ThreadGroups
                .Include(g => g.Threads)
                .Include(g => g.Category)
                .FirstOrDefaultAsync(g => g.Id == groupId);
        }

        public Task<ThreadReply> GetReplyByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Thread> GetThreadByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
