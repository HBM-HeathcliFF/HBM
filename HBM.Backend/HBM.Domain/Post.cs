using System.ComponentModel.DataAnnotations.Schema;

namespace HBM.Domain
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string CreationDate { get; set; }
        public string? EditDate { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }

        public List<Comment> Comments { get; set; } = new();
        public List<Reaction> Reactions { get; set; } = new();
    }
}