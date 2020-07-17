using AppLibrary.Core.Data.Interfaces;
using AppLibrary.Core.Models;
using AppLibrary.Core.Services.Interfaces;
using DataAccess.Core.Interface;
using DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLibrary.Core.Services
{
    public class UserActionService : IUserActionService
    {
        private readonly IDataAccess<UserActionDbModel> _userActionDataAccess;
        private IModelMapper<CourseModel, CourseDbModel> _modelMapper;
        private readonly IModelMapper<UserActionModel, UserActionDbModel> _userMapper;

        public UserActionService(IDataAccess<UserActionDbModel> userActionDataAccess,
                                  IModelMapper<CourseModel, CourseDbModel> modelMapper,
                                  IModelMapper<UserActionModel, UserActionDbModel> userMapper)
        {
            this._userActionDataAccess = userActionDataAccess;
            this._modelMapper = modelMapper;
            this._userMapper = userMapper;
        }

        public int Save(UserActionModel userAction)
        {
            var mappedCourse = _modelMapper.MapSingleDownwards(userAction.Course);
            var mappedUserAction = _userMapper.MapSingleDownwards(userAction);

            mappedUserAction.Course = mappedCourse;

            _userActionDataAccess.Add(mappedUserAction);
            _userActionDataAccess.Commint();

            return 0;
        }
    }
}
