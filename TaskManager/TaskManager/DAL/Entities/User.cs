using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using TaskManager.DAL.Entities;

namespace TaskApp.DAL.Entities
{
    public class User : Entity
    {
        [Display(Name = "Nombre usuario")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Password { get; set; }

        [Display(Name = "Grupo")]
        public Group Group { get; set; }

        [Display(Name = "Id grupo")]
        public Guid GroupId { get; set; }

        //a user can have n project
        public ICollection<Project> Projects { get; set; }

        //read property
        public int ProjectsNumber => Projects == null ? 0 : Projects.Count;
    }
}
