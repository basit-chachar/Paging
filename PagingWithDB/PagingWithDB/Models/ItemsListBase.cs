using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagingWithDB.Models
{
    /// <summary>
    /// this is a generic base class that can be derivered to create
    /// list of paginated items for any other type of classes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ItemsListBase<T> where T : class
    {
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalRecords { get; set; } = 0;
        public ICollection<T> Items { get; set; }
    }
}
