using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Core.Interface
{
    public interface IDataAccess<T>
        where T : class
    {
        IEnumerable<T> GetByName(string name);
        T GetById(int id);
        IEnumerable<T> GetRange();
        IEnumerable<T> GetCombinedList(int id);
        T Update(T updateObj);
        IEnumerable<T> Update(IEnumerable<T> updateObj);
        T DeleteRelated(T updateObj);
        T Add(T addObj);
        T Delete(int id);
        int GetCountOf();
        int Commint();
    }
}
