using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutryDairyASPApplication.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Paragraphs { get; set; }
        public string RelatedImagePath { get; set; }

        public int BlogId { get; set; }
        [ForeignKey("BlogId")]
        public Blog Blog { get; set; } 

    }
}
