using System.Collections.Generic;

namespace Cinema.SharedModels.BaseModels
{
    public class ListResult<T>
        where T : class, new()
    {
        public List<T> List { get; set; }

        public long TotalCount { get; set; }
    }
}
