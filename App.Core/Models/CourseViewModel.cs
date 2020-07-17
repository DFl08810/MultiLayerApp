using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Models
{
    public class CourseViewModel
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
        public List<UserViewModel> SignedUsers { get; set; }
        public bool IsOpened { get; set; }
    }
}
