using AppLibrary.Core.Models;
using DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLibrary.Core.Factories.Interfaces
{
    public interface ITrainCalendarFactory
    {
        CourseDbModel ConstructCourseForUpdate(CourseDbModel courseObject, List<UserActionModel> userActions)
    }
}
