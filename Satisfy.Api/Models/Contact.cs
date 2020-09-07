using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satisfy.Api.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        public User User { get; set; }

        public virtual List<Respondent> RespondentList { get; set; } = new List<Respondent>();
    }

}
