using AppLibrary.Core.Data.Interfaces;
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
    public class CourseDataService : ITrainCourseService
    {
        #region Fields
        private readonly IDataAccess<CourseDbModel> _courseDataAccess;
        private readonly IModelMapper<CourseModel, CourseDbModel> _modelMapper;
        private readonly IDataAccess<UserActionDbModel> _userDataAccess;
        private readonly IModelMapper<UserActionModel, UserActionDbModel> _userMapper;
        private readonly ITrainCalendarFactory _trainCalFactory;
        #endregion

        #region Constructor
        public CourseDataService(IDataAccess<CourseDbModel> courseDataAccess,
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
        #endregion

        #region Utilities
        #endregion

        #region Methods
        //Delete record from database
        public int Delete(int saveObjId)
        {
            _courseDataAccess.Delete(saveObjId);
            _courseDataAccess.Commint();
            return 0;
        }
        //Delete range of records from database
        public int Delete(IEnumerable<int> saveObjIdList)
        {
            throw new NotImplementedException();
        }
        //Get single record from database
        public CourseModel GetById(int objId)
        {
            var courseObj = _courseDataAccess.GetById(objId);
            var bindedCourseObj = _modelMapper.MapSingleUpwards(courseObj);
            return bindedCourseObj;
        }
        //Get range from database
        public IEnumerable<CourseModel> GetRange(bool removeDisabled = false)
        {
            var courseDownrangeList = _courseDataAccess.GetRange();
            //for removeDisabled option selected true, removes inactive courses from list 
            if (removeDisabled)
            {
                courseDownrangeList = courseDownrangeList.Where(isActive => isActive.IsOpened == true).ToList();
            }
            
            var courseModelList = _trainCalFactory.CreateFullCourseList(courseDownrangeList);
            return courseModelList;
        }

        //multi parameter Save method
        public int Save(CourseModel saveCourseModel, UserActionModel userActionModel, bool forUpdate = false, bool updateRelated = false)
        {
            //map data to data access layer
            var courseDbModel = _modelMapper.MapSingleDownwards(saveCourseModel);
            //initialize user action list and adding userActionModel 
            var courseActionList = new List<UserActionModel>();
            courseActionList.Add(userActionModel);

            //if updateRelated is true, merge userAction data into course object
            if (updateRelated)
            {
                //retrieves course with related user actions i.e. users registered to course
                //then converts list to single model
                var courseDbModels = _courseDataAccess.GetCombinedList(saveCourseModel.Id);
                courseDbModel = courseDbModels.FirstOrDefault();
                //use LINQ query to find specified user from userActionModel parameter
                //every user should be unique, so FirstOrDefault makes sure to stop after finding 1st result
                var queryFailResult = courseDbModel.UserActionModel.FirstOrDefault(search => search.UserName == userActionModel.UserName);

                //if query fail results in null, factory is called to construct merged course model with users
                if (queryFailResult == null)
                    courseDbModel = _trainCalFactory.ConstructCourseForUpdate(courseDbModel, courseActionList);
                else
                    return 1;
            }

            
            //updates existing or adds new record
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
                //TO DO
            }
            else
            {
                //TO DO
            }

            return 0;
        }

        public int DeleteRelatedUser(int courseId, UserActionModel userActionObject)
        {
            //get course list with related registered users
            var dbCoursesList = _courseDataAccess.GetCombinedList(courseId);
            //call factory and create mapped course object
            var mappedCourse = _trainCalFactory.CreateFullCourseList(dbCoursesList).FirstOrDefault();
            //update number of registered users in course
            mappedCourse.CourseCurrentCapacity = mappedCourse.UserActionModel.Where(x => x.UserName != userActionObject.UserName).ToList().Count();
            //get registered user that is going to get deleted and assign it to mapped course model
            mappedCourse.UserActionModel = mappedCourse.UserActionModel.Where(x => x.UserName == userActionObject.UserName).ToList();
            //map course to data access layer obj
            var courseForDb = _modelMapper.MapSingleDownwards(mappedCourse);
            //map registered user for deletion to data access layer obj rel.
            courseForDb.UserActionModel = _userMapper.MapRangeDownwards(mappedCourse.UserActionModel);

            //var actionId = mappedCourse.UserActionModel.FirstOrDefault().Id;
            //call data access layer
            try
            {
                _courseDataAccess.Update(courseForDb);
                _courseDataAccess.Commint();
                _courseDataAccess.DeleteRelated(courseForDb);
            }
            catch (Exception exc)
            {
                return 1;
            }
            _courseDataAccess.Commint();

            return 0;
        }
        #endregion
    }
}
