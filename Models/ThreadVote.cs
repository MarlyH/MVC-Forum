using Forum.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class ThreadVote
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; } // nullable in case user is deleted
        public User? User { get; set; }
        [ForeignKey("Thread")]
        public int ThreadId { get; set; }
        public Thread Thread { get; set; }
        public VoteType VoteType { get; set; }
    }
}
