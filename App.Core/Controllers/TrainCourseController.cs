using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Data;
using App.Core.Data.Interfaces;
using App.Core.Factories;
using App.Core.Models;
using AppLibrary.Core.Models;
using AppLibrary.Core.Services.Interfaces;
using IdentityAuthLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;

namespace App.Core.Controllers
{
    [Authorize]
    public class TrainCourseController : Controller
    {
        private readonly ITrainCourseService _trainCourseService;
        private readonly IUserActionService _userActionService;
        private readonly IViewModelMapper<CourseViewModel, CourseModel> _modelMapper;
        private readonly IViewModelMapper<UserViewModel, UserActionModel> _userMapper;
        private readonly ICourseViewFactory _courseFactory;
        private readonly UserManager<User> _userManager;

        public TrainCourseController(ITrainCourseService trainCourseService,
                                    IUserActionService userActionService,
                                    IViewModelMapper<CourseViewModel, CourseModel> modelMapper,
                                    IViewModelMapper<UserViewModel, UserActionModel> userMapper,
                                    ICourseViewFactory courseFactory,
                                    UserManager<IdentityAuthLib.Models.User> userManager)
        {
            this._trainCourseService = trainCourseService;
            this._userActionService = userActionService;
            this._modelMapper = modelMapper;
            this._userMapper = userMapper;
            this._courseFactory = courseFactory;
            this._userManager = userManager;
        }

        #region IndexActions
        public IActionResult Index()
        {
            var trainCourseList = _trainCourseService.GetRange();
            var trainCoursesMapped = new List<CourseViewModel>();
            //var trainBindedList = _dataMap.MapRangeUpwards(trainCourseList);
            //var trainCourseMapped = _modelMapper.MapRangeUpwards(trainCourseList);
            foreach (var item in trainCourseList)
            {
                var viewModel = new CourseViewModel();
                viewModel = _modelMapper.MapSingleUpwards(item);
                if (item.UserActionModel != null)
                {
                    viewModel.SignedUsers = _userMapper.MapRangeUpwards(item.UserActionModel).ToList();
                }
                trainCoursesMapped.Add(viewModel);
            }

            return View(trainCoursesMapped);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save([FromBody] CourseViewModel data)
        {
            if (data == null)
            {
                return StatusCode(400);
            }
            //Validates incoming datetime string user input
            #region DateValidation
            DateTime parsedDate;
            string pattern = "d.M.yyyy H:m:s";
            var isValidDateTime = DateTime.TryParseExact(data.FriendlyCourseDate, pattern, null, DateTimeStyles.None, out parsedDate);
            if (!isValidDateTime)
            {
                //return fail code
                return StatusCode(400);
            }
            else
            {
                //process data
                data.SystemCourseDate = parsedDate;
            }
            #endregion
            //commits verified record to database
            #region DatabaseAccess
            try
            {
                var AuthorizedUser = await _userManager.GetUserAsync(User);
                data.CreatedBy = AuthorizedUser.UserName;
                var courseData = _modelMapper.MapSingleDownwards(data);

                var UserAction = new UserActionModel() { UserName = data.CreatedBy, AuthSystemIdentity = AuthorizedUser.Id };

                _trainCourseService.Save(courseData, UserAction);
                //var courseData = _coursesService.BindCoursesModelToData(data);
                //courseData = _coursesData.Add(courseData);
                //_coursesData.Commint();
            }
            catch (Exception exc)
            {
                return StatusCode(500);
            }
            #endregion

            //return StatusCode(200);
            var returnMessage = "OK";
            return Json(returnMessage);
        }

        [HttpGet]
        public IActionResult GetById(int objId)
        {
            var courseModel = _trainCourseService.GetById(objId);
            var courseViewModel = _modelMapper.MapSingleUpwards(courseModel);
            //var obj = _coursesData.GetById(objId);
            return Json(courseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int objId)
        {
            if (objId == 0)
            {
                return StatusCode(400);
            }
            //retrieves deleted object and performs remove action from database
            #region DatabasAccess
            try
            {
                var obj = _trainCourseService.Delete(objId);
            }
            catch (Exception exc)
            {
                return StatusCode(500);
            }
            #endregion


            return StatusCode(200);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromBody] CourseViewModel data)
        {
            if (data == null)
            {
                return StatusCode(400);
            }
            //Validates incoming datetime string user input
            #region DateValidation
            DateTime parsedDate;
            string pattern = "d.M.yyyy H:m:s";
            var isValidDateTime = DateTime.TryParseExact(data.FriendlyCourseDate, pattern, null, DateTimeStyles.None, out parsedDate);
            if (!isValidDateTime)
            {
                //return fail code
                return StatusCode(400);
            }
            else
            {
                //process data
                data.SystemCourseDate = parsedDate;
            }
            #endregion
            //Validate Id
            if (data.Id == 0)
            {
                return StatusCode(400);
            }

            //commits verified record to database
            #region DatabaseAccess
            try
            {

                var UserAction = new UserActionModel() { UserName = "test", AuthSystemIdentity = "full" };

                var bindedCourseData = _modelMapper.MapSingleDownwards(data);
                _trainCourseService.Save(bindedCourseData, UserAction,true);
                //var courseData = _coursesService.BindCoursesModelToData(data);
                //courseData = _coursesData.Update(courseData);
                //_coursesData.Commint();
            }
            catch (Exception exc)
            {
                return StatusCode(500);
            }
            #endregion

            //return StatusCode(200);
            var returnMessage = "OK";
            return Json(returnMessage);
        }
        #endregion

        public async Task<IActionResult> CourseUserControl()
        {
            var AuthorizedUser = await _userManager.GetUserAsync(User);
            var UserAction = new UserActionModel() { UserName = AuthorizedUser.UserName, AuthSystemIdentity = AuthorizedUser.Id };

            var trainCourseList = _trainCourseService.GetRange();
            var trainCourseMapped = _courseFactory.CreateCourseForUserAction(trainCourseList, UserAction);
            //var trainCourseMapped = _modelMapper.MapRangeUpwards(trainCourseList);
            return View(trainCourseMapped);
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUserToTerm(int objId)
        {
            var AuthorizedUser = await _userManager.GetUserAsync(User);

            var courseModel = _trainCourseService.GetById(objId);

            var UserAction = new UserActionModel() { UserName = AuthorizedUser.UserName, AuthSystemIdentity = AuthorizedUser.Id, Course = courseModel };

            var actionStatus = _trainCourseService.Save(courseModel, UserAction, true, true);

            if(actionStatus == 0)
                return StatusCode(200);
            else
                return StatusCode(500);
        }

        

    }
}