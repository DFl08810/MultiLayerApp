using AppLibrary.Core.Models;
using DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLibrary.Core.Services.Interfaces
{
    public interface ITrainCourseService
    {
        IEnumerable<CourseModel> GetRange();
        int Save(CourseModel saveCoureModel, bool forUpdate = false);
        int Save(IEnumerable<CourseModel> saveCoureModelList, bool forUpdate = false);
    }
}
