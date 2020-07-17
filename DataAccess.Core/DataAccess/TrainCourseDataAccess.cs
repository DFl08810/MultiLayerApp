using DataAccess.Core.Interface;
using DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            Auxiliary.DetachAllEntities(db);
            var deleteList = updateObj.UserActionModel;
            //deleteList.FirstOrDefault().Course = updateObj;
            //db.RemoveRange(deleteList);
            db.UserActionEntries.RemoveRange(deleteList);
            //db.SaveChanges();
            return updateObj;
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
            List<CourseDbModel> coursesList = db.CoursesEntries.Where(ts => ts.Id == id).Include(x => x.UserActionModel).ToList();
            return coursesList;
        }

        public int GetCountOf()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CourseDbModel> GetRange()
        {
            return db.CoursesEntries.Include(x => x.UserActionModel).ToList();
        }

        public CourseDbModel Update(CourseDbModel updateObj)
        {
            Auxiliary.DetachAllEntities(db);

            db.CoursesEntries.Update(updateObj);
            return updateObj;
        }

        public IEnumerable<CourseDbModel> Update(IEnumerable<CourseDbModel> updateObj)
        {
            throw new NotImplementedException();
        }
    }
}
