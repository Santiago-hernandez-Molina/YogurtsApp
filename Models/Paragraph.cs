using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutryDairyASPApplication.Models
{
    public class Paragraph
    {
        [Key]
        public int Id { get; set; } 
        public string Content { get; set; }
        public int Order { get; set; }

        public int ArticleId { get; set; }
        [ForeignKey("ArticleId")]
        public Article Article { get; set; }
    }
}
