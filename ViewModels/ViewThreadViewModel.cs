using Forum.Models;
using Thread = Forum.Models.Thread;

namespace Forum.ViewModels
{
    public class ViewThreadViewModel
    {
        public Thread? Thread { get; set; }
        public string ReplyContent { get; set; }
        public int ThreadId { get; set; }
        public string? SignedInUserId { get; set; }
    }
}
