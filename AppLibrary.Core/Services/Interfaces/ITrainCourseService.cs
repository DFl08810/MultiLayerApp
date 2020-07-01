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
        int Save(CourseModel saveCourseModel, bool forUpdate = false);
        int Save(IEnumerable<CourseModel> saveCourseModelList, bool forUpdate = false);
        CourseModel GetById(int objId);

        int Delete(int saveObjId);
        int Delete(IEnumerable<int> saveObjIdList);

    }
}
