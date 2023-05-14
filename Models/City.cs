using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutryDairyASPApplication.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Region { get; set; }
        public string DaneCode { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }

}
