using System.ComponentModel.DataAnnotations;

namespace TaskManager.Entities
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; } /// <summary>
        /// el signo de interrogación significa que el campo puede ser nuleable
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
