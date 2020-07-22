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
    //Course view factory class is used to create view related objects 
    public class CourseViewFactory : ICourseViewFactory
    {
        #region Field
        private readonly IViewModelMapper<UserViewModel, UserActionModel> _userActionMap;
        private readonly IViewModelMapper<CourseViewModel, CourseModel> _courseMapper;
        #endregion

        #region Constructor
        public CourseViewFactory(IViewModelMapper<UserViewModel, UserActionModel> userActionMap,
                                 IViewModelMapper<CourseViewModel, CourseModel> courseMapper)
        {
            this._userActionMap = userActionMap;
            this._courseMapper = courseMapper;
        }
        #endregion

        #region Method
        //Creates list of courses with current active user, who requesed the list
        //If user is signed on course (is present in list of users) the list is swapped with current user
        //if user is not present, then the list is set to null
        public IEnumerable<CourseViewModel> CreateCourseForUserAction(IEnumerable<CourseModel> courseModels, UserActionModel userAction)
        {
            var outputList = new List<CourseViewModel>();
            foreach (var course in courseModels)
            {
                var courseViewModel = _courseMapper.MapSingleUpwards(course);

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

        #endregion
    }
}
