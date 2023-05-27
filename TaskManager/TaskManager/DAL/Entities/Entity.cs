using System.ComponentModel.DataAnnotations;

namespace TaskManager.DAL.Entities
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; } 
                                                   /// ?= nuleable
                                              
        public DateTime? ModifiedDate { get; set; }
    }
}
