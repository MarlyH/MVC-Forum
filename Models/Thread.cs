﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Thread
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(5000)]
        public string Content { get; set; }
        [ForeignKey("Author")] // the user who created the thread
        public string? AuthorId { get; set; } // nullable in case the user is deleted. 
        public User? Author { get; set; }
        public ICollection<ThreadReply> Replies { get; set; }
    }
}
