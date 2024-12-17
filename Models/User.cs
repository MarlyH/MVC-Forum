using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public ICollection<Thread> ThreadsAuthored { get; set; }
        public ICollection<ThreadReply> ThreadReplies { get; set; }
    }
}