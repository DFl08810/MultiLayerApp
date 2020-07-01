using AppLibrary.Core.Models;
using AppLibrary.Core.Services.Interfaces;
using DataAccess.Core.Interface;
using DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLibrary.Core.Services
{
    public class TrainCourseService : ITrainCourseService, IModelMapper<CourseModel, CourseDbModel>
    {
        private readonly IDataAccess<CourseDbModel> _courseDataAccess;

        public TrainCourseService(IDataAccess<CourseDbModel> courseDataAccess)
        {
            this._courseDataAccess = courseDataAccess;
        }

        public IEnumerable<CourseModel> GetRange()
        {
            var courseDownrangeList = _courseDataAccess.GetRange();
            var courseModelList = MapRangeUpwards(courseDownrangeList);

            return courseModelList;
        }

        public int SaveRange(List<CourseModel> courseModels)
        {

            return 0;
        }

        public IEnumerable<CourseDbModel> MapRangeDownwars(IEnumerable<CourseModel> modelRange)
        {
            var courseDbModelList = new List<CourseDbModel>();

            foreach(var courseModel in modelRange)
            {
                var courseDbModel = new CourseDbModel
                {
                    Id = courseModel.Id,
                    SystemCourseDate = courseModel.SystemCourseDate,
                    FriendlyCourseDate = courseModel.FriendlyCourseDate,
                    CourseName = courseModel.CourseName,
                    CourseLocation = courseModel.CourseLocation,
                    CourseShortDescription = courseModel.CourseShortDescription,
                    CourseCurrentCapacity = courseModel.CourseCurrentCapacity,
                    CourseMaxCapacity = courseModel.CourseMaxCapacity,
                    IsOpened = courseModel.IsOpened
                };

                courseDbModelList.Add(courseDbModel);
            }

            return courseDbModelList;
        }

        public IEnumerable<CourseModel> MapRangeUpwards(IEnumerable<CourseDbModel> modelRange)
        {
            var courseDbModelList = new List<CourseModel>();

            foreach (var courseDbModel in modelRange)
            {
                var courseModel = new CourseModel
                {
                    Id = courseDbModel.Id,
                    SystemCourseDate = courseDbModel.SystemCourseDate,
                    FriendlyCourseDate = courseDbModel.FriendlyCourseDate,
                    CourseName = courseDbModel.CourseName,
                    CourseLocation = courseDbModel.CourseLocation,
                    CourseShortDescription = courseDbModel.CourseShortDescription,
                    CourseCurrentCapacity = courseDbModel.CourseCurrentCapacity,
                    CourseMaxCapacity = courseDbModel.CourseMaxCapacity,
                    IsOpened = courseDbModel.IsOpened
                };

                courseDbModelList.Add(courseModel);
            }

            return courseDbModelList;
        }

        public int Save(CourseModel saveCoureModel, bool forUpdate = false)
        {
            throw new NotImplementedException();
        }

        public int Save(IEnumerable<CourseModel> saveCoureModelList, bool forUpdate = false)
        {
            throw new NotImplementedException();
        }
    }
}
