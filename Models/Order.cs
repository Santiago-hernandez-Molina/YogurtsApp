using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NutryDairyASPApplication.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public City City { get; set; }
        public string Address { get; set; }

        //Relationships
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public List<OrderItem> Items { get; set; }

    }
}
