using System;
using System.Collections.Generic;

namespace Mvc_Repository.Services.Misc
{
    public class Result : IResult
    {
        public Result(): this(false)
        {
        }

        public Result(bool success)
        {
            ID = Guid.NewGuid();
            Success = success;
            InnerResults = new List<IResult>();
        }

        public Guid ID { get; private set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public List<IResult> InnerResults { get; protected set; }
    }
}