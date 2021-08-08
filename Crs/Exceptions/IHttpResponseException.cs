using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Exceptions
{
    public interface IHttpResponseException
    {
        int Status { get; }

        object Value { get; }
    }
}
