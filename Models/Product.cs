using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutryDairyASPApplication.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        [Display(Name="Nombre")]
        public string Name { get; set; }

        [Display(Name="Descripción")]
        public string Description { get; set; }

        [Required]
        [Display(Name="Precio")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name="Calorias")]
        public decimal Calories { get; set; }

        [Required]
        [Display(Name="Grasas")]
        public decimal Fats { get; set; }

        [Required]
        [Display(Name="Proteinas")]
        public decimal Proteins { get; set; }

        [DefaultValue(0)]
        [Display(Name="Stock")]
        public int Stock { get; set; }

        public string ImagePath { get; set; }

        [NotMapped]
        [Display(Name="Imagen")]
        public IFormFile Archivo { get; set; }

        [Required]
        public int ProductSetId { get; set; }
        [ForeignKey("ProductSetId")]
        [Display(Name="Categoria")]
        public ProductSet ProductSet { get; set; }

        public List<Ingredient_Products> Ingredient_Products { get; set; }
    }
}
