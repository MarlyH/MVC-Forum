using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class ThreadGroup
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(100)]
        public string? Description { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public ThreadCategory Category { get; set; }
        public ICollection<Thread> Threads { get; set; }
    }
}
