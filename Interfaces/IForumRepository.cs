using Forum.Models;
using Thread = Forum.Models.Thread;

namespace Forum.Interfaces
{
    public interface IForumRepository
    {
        // Thread categories
        public Task<IEnumerable<ThreadCategory>> GetAllCategoriesAsync();
        public Task<ThreadCategory> GetCategoryByIdAsync(int categoryId);

        // Thread groups
        public Task<IEnumerable<ThreadGroup>> GetAllGroupsAsync();
        public Task<ThreadGroup> GetGroupByIdAsync(int id);
        public Task<IEnumerable<ThreadGroup>> GetGroupsByCategoryIdAsync(int categoryId);

        // Threads
        public Task<IEnumerable<Thread>> GetAllThreadsAsync();
        public Task<Thread> GetThreadByIdAsync(int id);
        public Task<IEnumerable<Thread>> GetThreadsByGroupIdAsync(int groupId);

        // Thread replies
        public Task<IEnumerable<ThreadReply>> GetRepliesByThreadIdAsync(int threadId);
        public Task<ThreadReply> GetReplyByIdAsync(int id);

        // Utility
        //Task<bool> CategoryExists(int categoryId);
        //Task<bool> GroupExists(int groupId);
        //Task<bool> ThreadExists(int threadId);
        //Task<bool> ReplyExists(int replyId);

        // Create
        public Task<bool> AddCategoryAsync(ThreadCategory category);
        public Task<bool> AddGroupAsync(ThreadGroup group);
        public Task<bool> AddThreadAsync(Thread thread);
        public Task<bool> AddReplyAsync(ThreadReply reply);
    }
}
