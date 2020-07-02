using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Data;
using App.Core.Data.Interfaces;
using App.Core.Models;
using AppLibrary.Core.Models;
using AppLibrary.Core.Services.Interfaces;
using IdentityAuthLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.Core.Controllers
{
    [Authorize]
    public class TrainCourseController : Controller
    {
        private readonly ITrainCourseService _trainCourseService;
        private readonly IViewModelMapper<CourseViewModel, CourseModel> _modelMapper;
        private readonly UserManager<User> _userManager;

        public TrainCourseController(ITrainCourseService trainCourseService,
                                    IViewModelMapper<CourseViewModel, CourseModel> modelMapper,
                                    UserManager<IdentityAuthLib.Models.User> userManager)
        {
            this._trainCourseService = trainCourseService;
            this._modelMapper = modelMapper;
            this._userManager = userManager;
        }

        public IActionResult Index()
        {
            var trainCourseList = _trainCourseService.GetRange();
            //var trainBindedList = _dataMap.MapRangeUpwards(trainCourseList);
            var trainCourseMapped = _modelMapper.MapRangeUpwards(trainCourseList);
            return View(trainCourseMapped);
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
                _trainCourseService.Save(courseData);
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
                var bindedCourseData = _modelMapper.MapSingleDownwards(data);
                _trainCourseService.Save(bindedCourseData, true);
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
    }
}