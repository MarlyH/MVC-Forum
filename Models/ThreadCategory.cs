using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Forum.Models
{
    public class ThreadCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(100)]
        public string? Description { get; set; }
        public ICollection<ThreadGroup> Groups { get; set; }

    }
}
