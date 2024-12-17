using Forum.Models;

namespace Forum.ViewModels
{
    public class ForumViewModel
    {
        public IEnumerable<ThreadCategory> Categories { get; set; }
        public IEnumerable<ThreadGroup> Groups { get; set; }
        public ThreadCategory Category { get; set; }
        public ThreadGroup Group { get; set; }
    }
}
