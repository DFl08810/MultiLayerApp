using App.Core.Data.Interfaces;
using App.Core.Models;
using AppLibrary.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Data
{
    public class UserViewModelMap : IViewModelMapper<UserViewModel, UserActionModel>
    {
        public IEnumerable<UserActionModel> MapRangeDownwards(IEnumerable<UserViewModel> modelRange)
        {
            var userActionList = new List<UserActionModel>();

            foreach (var courseModel in modelRange)
            {
                var userActionModel = MapSingleDownwards(courseModel);
                userActionList.Add(userActionModel);
            }

            return userActionList;
        }

        public IEnumerable<UserViewModel> MapRangeUpwards(IEnumerable<UserActionModel> modelRange)
        {
            var userViewList = new List<UserViewModel>();

            foreach (var courseDbModel in modelRange)
            {
                var courseModel = MapSingleUpwards(courseDbModel);
                userViewList.Add(courseModel);
            }

            return userViewList;
        }

        public UserActionModel MapSingleDownwards(UserViewModel singleModel)
        {
            var userActionModel = new UserActionModel
            {
                Id = singleModel.Id,
                UserName = singleModel.UserName
            };

            return userActionModel;
        }

        public UserViewModel MapSingleUpwards(UserActionModel singleModel)
        {
            var userViewModel = new UserViewModel
            {
                Id = singleModel.Id,
                UserName = singleModel.UserName
            };

            return userViewModel;
        }
    }
}
