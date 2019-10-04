using System;
using System.ComponentModel.DataAnnotations;
using DormyWebService.Entities.Account;

namespace DormyWebService.Entities
{
    public class EvaluationScoreHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Student TargetStudent { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public string Description { get; set; }

        [Required]
        public int ResultedScore { get; set; }
    }
}