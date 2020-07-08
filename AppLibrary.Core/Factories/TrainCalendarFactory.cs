using AppLibrary.Core.Data.Interfaces;
using AppLibrary.Core.Factories.Interfaces;
using AppLibrary.Core.Models;
using DataAccess.Core.Interface;
using DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLibrary.Core.Factories
{
    public class TrainCalendarFactory : ITrainCalendarFactory
    {
        private readonly IDataAccess<CourseDbModel> _courseDataAccess;
        private readonly IModelMapper<CourseModel, CourseDbModel> _modelMapper;
        private readonly IModelMapper<UserActionModel, UserActionDbModel> _userMapper;

        public TrainCalendarFactory(IDataAccess<CourseDbModel> courseDataAccess, IModelMapper<CourseModel, CourseDbModel> modelMapper,
                                  IModelMapper<UserActionModel, UserActionDbModel> userMapper)
        {
            this._courseDataAccess = courseDataAccess;
            this._modelMapper = modelMapper;
            this._userMapper = userMapper;
        }

        public CourseDbModel ConstructCourseForUpdate(CourseDbModel courseObject, List<UserActionModel> userActions)
        {
            //var mappedCourseObjects = _modelMapper.MapSingleDownwards(courseObject);
            var mappedUserActions = _userMapper.MapRangeDownwards(userActions);


            throw new NotImplementedException();
        }
    }
}
