using Forum.Models;
using System.ComponentModel.DataAnnotations;
using Thread = Forum.Models.Thread;

namespace Forum.ViewModels
{
    public class ViewThreadViewModel
    {
        public Thread? Thread { get; set; }
        [Required(ErrorMessage = "Reply must not be empty")]
        public string ReplyContent { get; set; }
        public int ThreadId { get; set; }
        public string? SignedInUserId { get; set; }
    }
}
