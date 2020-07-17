using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Core.Models
{
    public class CourseDbModel
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime SystemCourseDate { get; set; }
        public string FriendlyCourseDate { get; set; }
        public string CourseName { get; set; }
        public string CourseLocation { get; set; }
        public string CourseShortDescription { get; set; }
        public int CourseCurrentCapacity { get; set; }
        public int CourseMaxCapacity { get; set; }
        public bool IsOpened { get; set; }
        public IEnumerable<UserActionDbModel> UserActionModel { get; set; }
    }
}
