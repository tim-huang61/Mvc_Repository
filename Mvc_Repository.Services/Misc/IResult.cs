using System;
using System.Collections.Generic;

namespace Mvc_Repository.Services.Misc
{
    public interface IResult
    {
        Guid ID { get; }

        bool Success { get; set; }

        string Message { get; set; }

        Exception Exception { get; set; }

        List<IResult> InnerResults { get; }
    }
}