using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutryDairyASPApplication.Models
{
    public class Step
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int ElaborationProcessId { get; set; }
        [ForeignKey("ElaborationProcessId")]
        public ElaborationProcess ElaborationProcess { get; set; } 
    }
}