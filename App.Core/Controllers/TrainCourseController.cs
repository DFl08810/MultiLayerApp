﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Data;
using App.Core.Data.Interfaces;
using App.Core.Models;
using AppLibrary.Core.Models;
using AppLibrary.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Core.Controllers
{
    public class TrainCourseController : Controller
    {
        private readonly ITrainCourseService _trainCourseService;
        private readonly IViewModelMapper<CourseViewModel, CourseModel> _modelMapper;

        public TrainCourseController(ITrainCourseService trainCourseService, IViewModelMapper<CourseViewModel, CourseModel> modelMapper)
        {
            this._trainCourseService = trainCourseService;
            this._modelMapper = modelMapper;
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
        public IActionResult Save([FromBody] CourseViewModel data)
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
                var courseDataList = _modelMapper.MapSingleDownwards(data);
                _trainCourseService.
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
    }
}