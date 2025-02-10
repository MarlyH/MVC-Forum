using System.ComponentModel.DataAnnotations;

namespace Forum.ViewModels
{
    public class EditThreadViewModel
    {
        public Models.Thread? Thread { get; set; }
        public int ThreadId { get; set; }
        public string AuthorId { get; set; }
        public int GroupId { get; set; }
        [Required(ErrorMessage = "Post title must not be empty")]
        [StringLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Post body must not be empty")]
        [StringLength(5000)]
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
