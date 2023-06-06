using System.ComponentModel.DataAnnotations;


namespace NutryDairyASPApplication.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }

        public List<Article> Articles { get; set;}

    }
}
