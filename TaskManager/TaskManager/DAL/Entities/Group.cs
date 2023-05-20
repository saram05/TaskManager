using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using TaskApp.DAL.Entities;

namespace TaskManager.DAL.Entities
{
    public class Group : Entity
    {
        [Display(Name = "Grupo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "Usuarios")]
        public ICollection<User> Users { get; set; }
        //Propiedad de Lectura que no se mapea en la tabla de la DB
        public int UsersNumber => Users == null ? 0 : Users.Count; // If Ternario (? Entonces), (: sino)

    }
}
