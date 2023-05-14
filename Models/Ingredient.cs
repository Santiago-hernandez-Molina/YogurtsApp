using System.ComponentModel.DataAnnotations;


namespace NutryDairyASPApplication.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }

        public List<Ingredient_Products> IngredientProducts { get; set; }
    }
}
