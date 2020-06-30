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
        public IEnumerable<CourseModel> MapRangeDownwars(IEnumerable<CourseViewModel> modelRange)
        {
            var courseDbModelList = new List<CourseModel>();

            foreach (var courseModel in modelRange)
            {
                var courseDbModel = new CourseModel
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

        public IEnumerable<CourseViewModel> MapRangeUpwards(IEnumerable<CourseModel> modelRange)
        {
            var courseDbModelList = new List<CourseViewModel>();

            foreach (var courseDbModel in modelRange)
            {
                var courseModel = new CourseViewModel
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
    }
}
