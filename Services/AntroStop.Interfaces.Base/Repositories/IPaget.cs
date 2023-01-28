using System;
using System.Collections.Generic;

namespace AntroStop.Interfaces.Base.Repositories
{
    public interface IPaget<T>
    {
        IEnumerable<T> Items { get; }
        int TotalCount { get; }
        int PageIndex { get; }
        int PageSize { get; }
        int TotalPagesCount { get; }
    }
}
