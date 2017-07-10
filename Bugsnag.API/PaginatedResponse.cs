using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.API
{
    public class PaginatedResponse<T>
    {
        public PaginatedResponse(Uri nextLink, T result)
        {
            NextLink = nextLink;
            Result = result;
        }

        public PaginatedResponse(T result) : this(null, result) { }

        public Uri NextLink { get; }
        public T Result { get; }
    }
}
