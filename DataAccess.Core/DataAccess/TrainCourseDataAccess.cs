using DataAccess.Core.Interface;
using DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Core.DataAccess
{
    public class TrainCourseDataAccess : IDataAccess<CourseDbModel>
    {
        private DataContext db;

        public TrainCourseDataAccess(DataContext db)
        {
            this.db = db;
        }

        public CourseDbModel Add(CourseDbModel addObj)
        {
            db.Add(addObj);
            return addObj;
        }

        public int Commint()
        {
            return db.SaveChanges();
        }

        public CourseDbModel Delete(int id)
        {
            var deleteObject = GetById(id);
            if (deleteObject != null)
            {
                db.CoursesEntries.Remove(deleteObject);
            }
            return deleteObject;
        }

        public CourseDbModel DeleteRelated(CourseDbModel updateObj)
        {
            throw new NotImplementedException();
        }

        public CourseDbModel GetById(int id)
        {
            return db.CoursesEntries.Find(id);
        }

        public IEnumerable<CourseDbModel> GetByName(string name)
        {
            var query = from course in db.CoursesEntries select course;
            return query;
        }

        public IEnumerable<CourseDbModel> GetCombinedList(int id)
        {
            throw new NotImplementedException();
        }

        public int GetCountOf()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CourseDbModel> GetRange()
        {
            return db.CoursesEntries;
        }

        public CourseDbModel Update(CourseDbModel updateObj)
        {
            db.CoursesEntries.Update(updateObj);
            return updateObj;
        }

        public IEnumerable<CourseDbModel> Update(IEnumerable<CourseDbModel> updateObj)
        {
            throw new NotImplementedException();
        }
    }
}
