using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormyAppService.Models.AccountModels
{
    [Table("Students")]
    public class Student : ApplicationUser
    {
        public string StudentCardPictureUrl { get; set; }

        //Năm nhập học
        public int StartedSchoolYear { get; set; }

        //Khóa học
        public int Term { get; set; }

        public StudentPriorityType PriorityType { get; set; }

        //Phòng đang ở
        public Room Room { get; set; }

        public bool IsRoomLeader { get; set; }

        public int EvaluationScore { get; set; }

        //Điểm đánh giá
        public ICollection<EvaluationScoreHistory> EvaluationScoreHistory { get; set; }

    }
}