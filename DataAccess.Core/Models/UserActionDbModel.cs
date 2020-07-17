using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Core.Models
{
    public class UserActionDbModel
    {
        public int Id { get; set; }
        public string AuthSystemIdentity { get; set; }
        public string UserName { get; set; }
        public CourseDbModel Course { get; set; }

    }
}
