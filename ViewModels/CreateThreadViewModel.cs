using Forum.Models;
using System.ComponentModel.DataAnnotations;

namespace Forum.ViewModels
{
    public class CreateThreadViewModel // TODO: validation is shared between this viewmodel and the Thread model. better approach?
    {
        public int PassedGroupId { get; set; }
        public int GroupId { get; set; }
        [Required(ErrorMessage = "Post title must not be empty")]
        [StringLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Post body must not be empty")]
        [StringLength(5000)]
        public string Content { get; set; }
        public IEnumerable<ThreadGroup>? Groups { get; set; }
    }
}
