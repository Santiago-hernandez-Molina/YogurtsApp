using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NutryDairyASPApplication.Models
{
    public class ElaborationProcess
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
 
        public List<Step> Steps { get; set; }

    }
}
