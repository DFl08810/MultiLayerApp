using DataAccess.Core.Interface;
using DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Core.DataAccess
{
    public class UserActionDataAccess : IDataAccess<UserActionDbModel>
    {
        private DataContext db;

        public UserActionDataAccess(DataContext db)
        {
            this.db = db;
        }

        public UserActionDbModel Add(UserActionDbModel addObj)
        {
            db.Attach(addObj);
            
            
            return addObj;
        }

        public int Commint()
        {
            return db.SaveChanges();
        }

        public UserActionDbModel Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UserActionDbModel DeleteRelated(UserActionDbModel updateObj)
        {
            throw new NotImplementedException();
        }

        public UserActionDbModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserActionDbModel> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserActionDbModel> GetCombinedList(int id)
        {
            throw new NotImplementedException();
        }

        public int GetCountOf()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserActionDbModel> GetRange()
        {
            throw new NotImplementedException();
        }

        public UserActionDbModel Update(UserActionDbModel updateObj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserActionDbModel> Update(IEnumerable<UserActionDbModel> updateObj)
        {
            throw new NotImplementedException();
        }
    }
}
