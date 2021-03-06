using AppLibrary.Core.Models;
using DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLibrary.Core.Services.Interfaces
{
    public interface ITrainCourseService
    {
        IEnumerable<CourseModel> GetRange(bool removeDisabled = false);
        int Save(CourseModel saveCourseModel, UserActionModel userActionModel, bool forUpdate = false, bool updateRelated = false);
        int Save(IEnumerable<CourseModel> saveCourseModelList, bool forUpdate = false);
        CourseModel GetById(int objId);

        int Delete(int saveObjId);
        int Delete(IEnumerable<int> saveObjIdList);
        int DeleteRelatedUser(int courseId, UserActionModel userActionObject);
    }
}
