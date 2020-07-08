﻿using AppLibrary.Core.Data.Interfaces;
using AppLibrary.Core.Models;
using AppLibrary.Core.Services.Interfaces;
using DataAccess.Core.Interface;
using DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLibrary.Core.Services
{
    public class TrainCourseService : ITrainCourseService
    {
        private readonly IDataAccess<CourseDbModel> _courseDataAccess;
        private readonly IModelMapper<CourseModel, CourseDbModel> _modelMapper;
        private readonly IModelMapper<UserActionModel, UserActionDbModel> _userMapper;

        public TrainCourseService(IDataAccess<CourseDbModel> courseDataAccess,
                                  IModelMapper<CourseModel, CourseDbModel> modelMapper,
                                  IModelMapper<UserActionModel, UserActionDbModel> userMapper)
        {
            this._courseDataAccess = courseDataAccess;
            this._modelMapper = modelMapper;
            this._userMapper = userMapper;
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
            var courseModelList = _modelMapper.MapRangeUpwards(courseDownrangeList);

            return courseModelList;
        }

        public int Save(CourseModel saveCourseModel, UserActionModel userActionModel, bool forUpdate = false)
        {

            var courseDbModel = _modelMapper.MapSingleDownwards(saveCourseModel);

            var courseDbModels = _courseDataAccess.GetCombinedList(saveCourseModel.Id);
            courseDbModel = courseDbModels.FirstOrDefault();


            var courseActionList = new List<UserActionModel>();
            //courseActionList.Add(userActionModel);
            courseActionList.Add(userActionModel);

            var userAction = _userMapper.MapRangeDownwards(courseActionList);

            var userlist = courseDbModel.UserActionModel.ToList();
            userlist.AddRange(userAction);

            //userAction.Append(courseDbModel.UserActionModel.FirstOrDefault());

            courseDbModel.UserActionModel = userlist;
            

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
    }
}
