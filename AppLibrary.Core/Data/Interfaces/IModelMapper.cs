using System;
using System.Collections.Generic;
using System.Text;

namespace AppLibrary.Core.Data.Interfaces
{
    public interface IModelMapper<T, K>
        where T : class
        where K : class
    {
        T MapSingleUpwards(K singleModel);
        K MapSingleDownwards(T singleModel);
        IEnumerable<T> MapRangeUpwards(IEnumerable<K> modelRange);
        IEnumerable<K> MapRangeDownwards(IEnumerable<T> modelRange);
    }
}
