using App.Core.Data.Interfaces;
using App.Core.Models;
using AppLibrary.Core.Models;
using IdentityAuthLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Factories
{
    public class CourseViewFactory : ICourseViewFactory
    {
        private readonly IViewModelMapper<UserViewModel, UserActionModel> _userActionMap;
        private readonly IViewModelMapper<CourseViewModel, CourseModel> _courseMap;

        public CourseViewFactory(IViewModelMapper<UserViewModel, UserActionModel> userActionMap,
                                 IViewModelMapper<CourseViewModel, CourseModel> courseMap)
        {
            this._userActionMap = userActionMap;
            this._courseMap = courseMap;
        }

        public IEnumerable<CourseViewModel> CreateCourseForUserAction(IEnumerable<CourseModel> courseModels, UserActionModel userAction)
        {
            var outputList = new List<CourseViewModel>();
            foreach (var course in courseModels)
            {
                var courseViewModel = _courseMap.MapSingleUpwards(course);

                var findResult = course.UserActionModel.FirstOrDefault(search => search.UserName == userAction.UserName);
                if (findResult != null)
                {
                    var userViewAction = _userActionMap.MapSingleUpwards(userAction);
                    courseViewModel.SignedUsers = new List<UserViewModel>();
                    courseViewModel.SignedUsers.Add(userViewAction);
                }

                outputList.Add(courseViewModel);
            }

            return outputList;
        }
    }
}
