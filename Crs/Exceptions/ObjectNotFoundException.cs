using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Exceptions
{
    public class ObjectNotFoundException : Exception, IHttpResponseException
    {
        public int Status => StatusCodes.Status404NotFound;

        public object Value => "Nie znaleziono obiektu";
    }
}
