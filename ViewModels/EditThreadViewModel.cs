namespace Forum.ViewModels
{
    public class EditThreadViewModel
    {
        public Models.Thread? Thread { get; set; }
        public int ThreadId { get; set; }
        public string AuthorId { get; set; }
        public int GroupId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
