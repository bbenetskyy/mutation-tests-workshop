using System;
using System.Collections.Generic;
using System.Text;

namespace RzeszowBusCore.Services.Abstract
{
    public interface IFilter<T> where T : class
    {
        List<T> FilterBy(List<T> items, List<string> filters);
    }
}
