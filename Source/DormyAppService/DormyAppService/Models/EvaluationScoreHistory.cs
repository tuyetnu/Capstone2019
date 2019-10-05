﻿using System;
using System.ComponentModel.DataAnnotations;
using DormyAppService.Models.AccountModels;

namespace DormyAppService.Models
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