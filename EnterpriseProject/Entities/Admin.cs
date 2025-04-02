using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseProject.Entities
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set;  }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }  // Navigation property
    }
}
