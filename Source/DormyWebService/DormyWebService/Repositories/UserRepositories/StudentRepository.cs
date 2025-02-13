﻿using DormyWebService.Entities;
using DormyWebService.Entities.AccountEntities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormyWebService.Repositories.UserRepositories
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(DormyDbContext context) : base(context)
        {
        }

        public Student FindWithRoomBydId(int id)
        {
            return Context.Students.Include(r => r.Room).FirstOrDefault(s => s.StudentId == id);
        }

        public List<Student> GetAllStudentInDormitoryIncluding()
        {
            return Context.Students
                .Include(r => r.Room)
                .Where(s => s.RoomId != null)
                .ToList();
        }
    }
}