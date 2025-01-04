using Forum.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Thread = Forum.Models.Thread;

namespace Forum.ViewModels
{
    public class CreateThreadViewModel
    {
        public Thread Thread { get; set; }
        public int PassedGroupId { get; set; }
        public IEnumerable<ThreadGroup> Groups { get; set; }
    }
}
