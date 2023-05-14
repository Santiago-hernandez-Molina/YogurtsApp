using NutryDairyASPApplication.Models;

namespace NutryDairyASPApplication.ViewModels
{
    public class AdminVM
    {
        public List<Product> Products { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<City> Cities { get; set; }
        public List<Article> Articles { get; set; }
        public List<ElaborationProcess> ElaborationProcesses { get; set; }
    }
}
