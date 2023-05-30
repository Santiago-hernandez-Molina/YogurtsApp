using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NutryDairyASPApplication.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }

        public int Amount { get; set; }
        public decimal Price { get; set; }

        // Relationships
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

    }
}
