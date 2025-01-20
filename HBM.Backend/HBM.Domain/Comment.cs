using System.ComponentModel.DataAnnotations.Schema;

namespace HBM.Domain
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }

        public Guid PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}