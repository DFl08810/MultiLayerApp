using AppLibrary.Core.Data.Interfaces;
using AppLibrary.Core.Models;
using DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLibrary.Core.Data
{
    public class CourseDataMap : IModelMapper<CourseModel, CourseDbModel>
    {
        public IEnumerable<CourseDbModel> MapRangeDownwards(IEnumerable<CourseModel> modelRange)
        {
            var courseDbModelList = new List<CourseDbModel>();

            foreach (var courseModel in modelRange)
            {
                var courseDbModel = MapSingleDownwards(courseModel);

                courseDbModelList.Add(courseDbModel);
            }

            return courseDbModelList;
        }

        public IEnumerable<CourseModel> MapRangeUpwards(IEnumerable<CourseDbModel> modelRange)
        {
            var courseDbModelList = new List<CourseModel>();

            foreach (var courseDbModel in modelRange)
            {

                var courseModel = MapSingleUpwards(courseDbModel);
                courseDbModelList.Add(courseModel);
            }

            return courseDbModelList;
        }

        public CourseDbModel MapSingleDownwards(CourseModel singleModel)
        {
            var courseDbModel = new CourseDbModel
            {
                Id = singleModel.Id,
                CreatedBy = singleModel.CreatedBy,
                SystemCourseDate = singleModel.SystemCourseDate,
                FriendlyCourseDate = singleModel.FriendlyCourseDate,
                CourseName = singleModel.CourseName,
                CourseLocation = singleModel.CourseLocation,
                CourseShortDescription = singleModel.CourseShortDescription,
                CourseCurrentCapacity = singleModel.CourseCurrentCapacity,
                CourseMaxCapacity = singleModel.CourseMaxCapacity,
                IsOpened = singleModel.IsOpened
            };

            return courseDbModel;
        }

        public CourseModel MapSingleUpwards(CourseDbModel singleModel)
        {
            var courseModel = new CourseModel
            {
                Id = singleModel.Id,
                CreatedBy = singleModel.CreatedBy,
                SystemCourseDate = singleModel.SystemCourseDate,
                FriendlyCourseDate = singleModel.FriendlyCourseDate,
                CourseName = singleModel.CourseName,
                CourseLocation = singleModel.CourseLocation,
                CourseShortDescription = singleModel.CourseShortDescription,
                CourseCurrentCapacity = singleModel.CourseCurrentCapacity,
                CourseMaxCapacity = singleModel.CourseMaxCapacity,
                IsOpened = singleModel.IsOpened
            };

            return courseModel;
        }
    }
}
