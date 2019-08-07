using System.Collections.Generic;

namespace RzeszowBusCore.Services.Abstract
{
    public interface ITrim<T> where T : class
    {
        List<T> Trim(List<T> items, int maxLength);
    }
}