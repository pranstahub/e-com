using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Api.Common.General
{
    public abstract class Result
    {
        public List<Errors> Errors { get; set; }
        public bool IsError => Errors != null && Errors.Any();
    }

    public class Result<T> : Result
    {
        public T Response { get; set; }
        public string WarningMessage { get; set; }
    }
}
