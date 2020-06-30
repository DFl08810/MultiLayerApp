using System;
using System.Collections.Generic;
using System.Text;

namespace AppLibrary.Core.Services.Interfaces
{
    public interface IModelMapper<T, K>
        where T : class
        where K : class
    {
        IEnumerable<T> MapRangeUpwards(IEnumerable<K> modelRange);
        IEnumerable<K> MapRangeDownwars(IEnumerable<T> modelRange);
    }
}
