using EFDataStorage.Entities;
using EFDataStorage.Patterns;
using System.Collections.Generic;

namespace EFDataStorage.Helper
{
    public class PagingRequest : QueryFor<IEnumerable<User>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string SearchString { get; set; }
    }
}
