using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SeedWork
{
    public class ApiResult<T>
    {
        public ApiResult()
        {

        }

        public ApiResult(bool isSucceed, string message = null)
        {
            Message = message;
            IsSucceed = isSucceed;
        }

        public ApiResult(bool isSucceed, T data, string message = null)
        {
            Data = data;
            Message = message;
            IsSucceed=isSucceed;

        }

        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
