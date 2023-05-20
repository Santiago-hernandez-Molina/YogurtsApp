using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutryDairyASPApplication.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Display(Name = "Dirección")]
        public string Address { get; set; }

        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }

    }
}
