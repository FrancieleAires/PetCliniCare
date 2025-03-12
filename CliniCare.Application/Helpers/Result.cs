using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Helpers
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }
        public T Value { get; set; }
        public string Token { get; set; }

        public static Result<T> Success(T value, string token = null)
        {
            return new Result<T> { IsSuccess = true, Value = value, Token = token };
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T> { IsSuccess = false, Errors = new[] { error } };
        }
    }
}
