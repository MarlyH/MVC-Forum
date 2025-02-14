﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Thread
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Post title must not be empty")]
        [StringLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Post body must not be empty")]
        [StringLength(5000)]
        public string Content { get; set; }
        [Required()]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateLastEdited { get; set; }
        [ForeignKey("Author")] // the user who created the thread
        public string? AuthorId { get; set; } // nullable in case the user is deleted. 
        public User? Author { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public ThreadGroup Group { get; set; }
        public ICollection<ThreadReply>? Replies { get; set; }
        public int Upvotes { get; set; } = 0;
        public int Downvotes { get; set; } = 0;
        public ICollection<ThreadVote> Votes { get; set; }
    }
}
