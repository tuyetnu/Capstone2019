using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormyWebService.Entities.Account
{
    public class Student
    {
        //Foreign key is also Key, point's to id in account table
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountId { get; set; }

        public Account Account { get; set; }

        public string StudentCardPictureUrl { get; set; }

        //Năm nhập học
        public int StartedSchoolYear { get; set; }

        //Khóa học
        public int Term { get; set; }

        public StudentPriorityType PriorityType { get; set; }

        //Phòng đang ở
        public Room.Room Room { get; set; }

        public bool IsRoomLeader { get; set; }

        //Điểm đánh giá
        private ICollection<EvaluationScoreHistory> EvaluationScoreHistory { get; set; }
    }
}