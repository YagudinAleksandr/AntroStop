using AntroStop.Domain.Pagination.RequestFeatures;
using System.Collections.Generic;

namespace AntroStop.Domain.Pagination.Features
{
    public class PagingResponse<T> where T : class
    {
        public List<T> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}
