using AppLibrary.Core.Data.Interfaces;
using AppLibrary.Core.Factories.Interfaces;
using AppLibrary.Core.Models;
using DataAccess.Core.Interface;
using DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLibrary.Core.Factories
{
    //these factories are used for constructing objects in logic layer
    //they abstract mapping process
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

            var oldUserActions = courseObject.UserActionModel.ToList();
            oldUserActions.AddRange(mappedUserActions);

            courseObject.UserActionModel = oldUserActions;

            courseObject.CourseCurrentCapacity = courseObject.UserActionModel.Count();

            return courseObject;
        }

        public IEnumerable<CourseModel> CreateFullCourseList(IEnumerable<CourseDbModel> courseList)
        {
            var outputList = new List<CourseModel>();
            
            foreach (var item in courseList)
            {
                var courseMapped = new CourseModel();
                courseMapped = _modelMapper.MapSingleUpwards(item);
                if(item.UserActionModel != null)
                {
                    courseMapped.UserActionModel = _userMapper.MapRangeUpwards(item.UserActionModel);
                }

                outputList.Add(courseMapped);
            }

            return outputList;
        }
    }
}
