using App.Core.Models;
using AppLibrary.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Factories
{
    public interface ICourseViewFactory
    {
        IEnumerable<CourseViewModel> CreateCourseForUserAction(IEnumerable<CourseModel> courseModels, UserActionModel userAction);
    }
}
