﻿using AppLibrary.Core.Data.Interfaces;
using AppLibrary.Core.Models;
using DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLibrary.Core.Data
{
    public class UserActionDataMap : IModelMapper<UserActionModel, UserActionDbModel>
    {
        public IEnumerable<UserActionDbModel> MapRangeDownwards(IEnumerable<UserActionModel> modelRange)
        {
            var courseDbModelList = new List<UserActionDbModel>();

            foreach (var courseModel in modelRange)
            {
                var courseDbModel = MapSingleDownwards(courseModel);

                courseDbModelList.Add(courseDbModel);
            }

            return courseDbModelList;
        }

        public IEnumerable<UserActionModel> MapRangeUpwards(IEnumerable<UserActionDbModel> modelRange)
        {
            var courseDbModelList = new List<UserActionModel>();

            foreach (var courseDbModel in modelRange)
            {

                var courseModel = MapSingleUpwards(courseDbModel);
                courseDbModelList.Add(courseModel);
            }

            return courseDbModelList;
        }

        public UserActionDbModel MapSingleDownwards(UserActionModel singleModel)
        {
            var courseDbModel = new UserActionDbModel
            {
                Id = singleModel.Id,
                Course = singleModel.Course,
                AuthSystemIdentity = singleModel.AuthSystemIdentity,
                UserName = singleModel.UserName
            };

            return courseDbModel;
        }

        public UserActionModel MapSingleUpwards(UserActionDbModel singleModel)
        {
            var courseModel = new UserActionModel
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