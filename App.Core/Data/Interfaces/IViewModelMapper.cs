using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Data.Interfaces
{
    public interface IViewModelMapper<T, K>
        where T : class
        where K : class
    {
        IEnumerable<T> MapRangeUpwards(IEnumerable<K> modelRange);
        IEnumerable<K> MapRangeDownwars(IEnumerable<T> modelRange);
    }
}
