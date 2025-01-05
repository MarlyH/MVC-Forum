using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class ThreadReply
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(5000)]
        public string Content { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateLastEdited { get; set; }
        [ForeignKey("Thread")] // the thread associated with the reply
        public int ThreadId { get; set; }
        public Thread Thread { get; set; }
        [ForeignKey("Author")] // the user who created the reply
        public string? AuthorId { get; set; } // nullable in case the user is deleted. 
        public User? Author { get; set; }
    }
}