using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satisfy.Api.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int AnswerPosition { get; set; }
        [Required]
        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }
        [Required]
        public int Type { get; set; }
        public Question Question { get; set; }
        public virtual List<Response> ResponseList { get; set; } = new List<Response>();
    }
}