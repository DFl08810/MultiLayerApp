using App.Core.Data.Interfaces;
using App.Core.Models;
using AppLibrary.Core.Models;
using AppLibrary.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Data
{
    public class CourseDataMap : IViewModelMapper<CourseViewModel, CourseModel>
    {
        public CourseModel MapSingleDownwards(CourseViewModel singleModel)
        {
            var courseDbModel = new CourseModel
            {
                Id = singleModel.Id,
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

        public CourseViewModel MapSingleUpwards(CourseModel singleModel)
        {
            var courseModel = new CourseViewModel
            {
                Id = singleModel.Id,
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

        public IEnumerable<CourseModel> MapRangeDownwards(IEnumerable<CourseViewModel> modelRange)
        {
            var courseDbModelList = new List<CourseModel>();

            foreach (var courseModel in modelRange)
            {
                var courseDbModel = MapSingleDownwards(courseModel);
                courseDbModelList.Add(courseDbModel);
            }

            return courseDbModelList;
        }

        public IEnumerable<CourseViewModel> MapRangeUpwards(IEnumerable<CourseModel> modelRange)
        {
            var courseDbModelList = new List<CourseViewModel>();

            foreach (var courseDbModel in modelRange)
            {
                var courseModel = MapSingleUpwards(courseDbModel);
                courseDbModelList.Add(courseModel);
            }

            return courseDbModelList;
        }

    }
}
