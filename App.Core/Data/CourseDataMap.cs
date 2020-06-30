using App.Core.Models;
using AppLibrary.Core.Models;
using AppLibrary.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Data
{
    public class CourseDataMap : IModelMapper<CourseViewModel, CourseModel>
    {
        public IEnumerable<CourseModel> MapRangeDownwars(IEnumerable<CourseViewModel> modelRange)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CourseViewModel> MapRangeUpwards(IEnumerable<CourseModel> modelRange)
        {
            throw new NotImplementedException();
        }
    }
}
