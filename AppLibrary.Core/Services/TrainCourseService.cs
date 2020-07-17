﻿using AppLibrary.Core.Data.Interfaces;
using AppLibrary.Core.Factories.Interfaces;
using AppLibrary.Core.Models;
using AppLibrary.Core.Services.Interfaces;
using DataAccess.Core.Interface;
using DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AppLibrary.Core.Services
{
    public class TrainCourseService : ITrainCourseService
    {
        private readonly IDataAccess<CourseDbModel> _courseDataAccess;
        private readonly IModelMapper<CourseModel, CourseDbModel> _modelMapper;
        private readonly IDataAccess<UserActionDbModel> _userDataAccess;
        private readonly IModelMapper<UserActionModel, UserActionDbModel> _userMapper;
        private readonly ITrainCalendarFactory _trainCalFactory;

        public TrainCourseService(IDataAccess<CourseDbModel> courseDataAccess,
                                  IDataAccess<UserActionDbModel> userDataAccess,
                                  IModelMapper<CourseModel, CourseDbModel> modelMapper,
                                  IModelMapper<UserActionModel, UserActionDbModel> userMapper,
                                  ITrainCalendarFactory trainCalendarFactory)
        {
            this._courseDataAccess = courseDataAccess;
            this._modelMapper = modelMapper;
            this._userDataAccess = userDataAccess;
            this._userMapper = userMapper;
            this._trainCalFactory = trainCalendarFactory;
        }

        public int Delete(int saveObjId)
        {
            _courseDataAccess.Delete(saveObjId);
            _courseDataAccess.Commint();
            return 0;
        }

        public int Delete(IEnumerable<int> saveObjIdList)
        {
            throw new NotImplementedException();
        }

        public CourseModel GetById(int objId)
        {
            var courseObj = _courseDataAccess.GetById(objId);
            var bindedCourseObj = _modelMapper.MapSingleUpwards(courseObj);
            return bindedCourseObj;
        }

        public IEnumerable<CourseModel> GetRange()
        {
            var courseDownrangeList = _courseDataAccess.GetRange();
            //var courseModelList = _modelMapper.MapRangeUpwards(courseDownrangeList);
            var courseModelList = _trainCalFactory.CreateFullCourseList(courseDownrangeList);
            return courseModelList;
        }

        public int Save(CourseModel saveCourseModel, UserActionModel userActionModel, bool forUpdate = false, bool updateRelated = false)
        {

            var courseDbModel = _modelMapper.MapSingleDownwards(saveCourseModel);

            var courseActionList = new List<UserActionModel>();
            courseActionList.Add(userActionModel);

            //courseDbModel = _trainCalFactory.ConstructCourseForUpdate(courseDbModel, courseActionList);

            if (updateRelated)
            {
                var courseDbModels = _courseDataAccess.GetCombinedList(saveCourseModel.Id);
                courseDbModel = courseDbModels.FirstOrDefault();
                var testResult = courseDbModel.UserActionModel.FirstOrDefault(search => search.UserName == userActionModel.UserName);

                if (testResult == null)
                    courseDbModel = _trainCalFactory.ConstructCourseForUpdate(courseDbModel, courseActionList);
                else
                    return 1;
                //var testResult = courseDbModel.UserActionModel.FirstOrDefault(search => search.UserName == userActionModel.UserName);
            }

            

            if (!forUpdate)
            {
                _courseDataAccess.Add(courseDbModel);
            }
            else
            {
                _courseDataAccess.Update(courseDbModel);
            }

            _courseDataAccess.Commint();
            return 0;
        }

        public int Save(IEnumerable<CourseModel> saveCourseModelList, bool forUpdate = false)
        {
            var courseDbModels = _modelMapper.MapRangeDownwards(saveCourseModelList);

            if (forUpdate)
            {

            }
            else
            {

            }

            return 0;
        }

        public int DeleteRelatedUser(int courseId, UserActionModel userActionObject)
        {
            var dbCoursesList = _courseDataAccess.GetCombinedList(courseId);
            var mappedCourse = _trainCalFactory.CreateFullCourseList(dbCoursesList).FirstOrDefault();

            mappedCourse.CourseCurrentCapacity = mappedCourse.UserActionModel.Where(x => x.UserName != userActionObject.UserName).ToList().Count();

            mappedCourse.UserActionModel = mappedCourse.UserActionModel.Where(x => x.UserName == userActionObject.UserName).ToList();
            //mappedCourse.CourseCurrentCapacity -= 1;

            mappedCourse.UserActionModel = mappedCourse.UserActionModel;

            var courseForDb = _modelMapper.MapSingleDownwards(mappedCourse);

            courseForDb.UserActionModel = _userMapper.MapRangeDownwards(mappedCourse.UserActionModel);

            //_courseDataAccess.DeleteRelated(courseForDb);
            var actionId = mappedCourse.UserActionModel.FirstOrDefault().Id;
            try
            {
                //_userDataAccess.Delete(actionId);
                _courseDataAccess.Update(courseForDb);
                _courseDataAccess.Commint();
                _courseDataAccess.DeleteRelated(courseForDb);
            }
            catch (Exception exc)
            {
                return 1;
            }
            _courseDataAccess.Commint();
            //_userDataAccess.Commint();
            //mappedCourse.UserActionModel == newUserList

            return 0;
        }
    }
}
