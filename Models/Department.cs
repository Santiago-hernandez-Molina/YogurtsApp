using System.ComponentModel.DataAnnotations;

namespace NutryDairyASPApplication.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string DaneCode { get; set; }
        public string Name { get; set; }
        public List<City> cities { get; set; }
    }

}
