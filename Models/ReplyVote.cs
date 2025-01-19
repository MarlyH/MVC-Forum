using Forum.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class ReplyVote
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; } // nullable in case user is deleted
        public User? User { get; set; }
        [ForeignKey("Reply")]
        public int ReplyId { get; set; }
        public ThreadReply Reply { get; set; }
        public VoteType VoteType { get; set; }
    }
}
