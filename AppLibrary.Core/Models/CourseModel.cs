using System;
using System.Collections.Generic;
using System.Text;

namespace AppLibrary.Core.Models
{
    public class CourseModel
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
    }
}
