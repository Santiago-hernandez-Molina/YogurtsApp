using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutryDairyASPApplication.Models
{
    public class Ingredient_Products{
        [Key]
        public int Id { get; set;}

        public int ProductId { get; set;}
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int IngredientId { get; set;}
        [ForeignKey("IngredientId")]
        public Ingredient Ingredient { get; set; }

    }
}
