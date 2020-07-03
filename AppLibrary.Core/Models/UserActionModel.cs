using System;
using System.Collections.Generic;
using System.Text;

namespace AppLibrary.Core.Models
{
    public class UserActionModel
    {
        public int Id { get; set; }
        public string AuthSystemIdentity { get; set; }
        public string UserName { get; set; }
        public CourseModel Course { get; set; }
    }
}
