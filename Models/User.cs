using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class User : IdentityUser
    {
        /*[Key]
        public string Id { get; set; }*/
        public ICollection<Thread> ThreadsAuthored { get; set; }
        public ICollection<ThreadReply> ThreadReplies { get; set; }
    }
}