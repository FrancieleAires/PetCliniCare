using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Helpers
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }
        public string Value { get; set; }
        public string Token { get; set; }

        public static Result Success(string value, string token = null)
        {
            return new Result { IsSuccess = true, Value = value, Token = token };
        }

        public static Result Failure(string error)
        {
            return new Result { IsSuccess = false, Errors = new[] { error } };
        }
    }
}
