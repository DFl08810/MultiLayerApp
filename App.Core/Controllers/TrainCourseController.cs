using System;
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
    }
}