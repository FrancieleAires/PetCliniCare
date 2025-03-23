using Microsoft.AspNetCore.Mvc;
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

        public List<Error>? ErrorClass { get; set; } 

        public static Result<T> Success(T value, string token = null)
        {
            return new Result<T> { 
                IsSuccess = true,
                Value = value, 
                Token = token };
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T> { 
                IsSuccess = false,
                Errors = new[] { error } };
        }
        public static Result<T> Failure(string message, ErrorType errorType)
        {
            return new Result<T> {
                IsSuccess = false,
                ErrorClass = new List<Error>{

                    new Error
                    {
                        Message = message,
                        Type = errorType
                    }
            
                }
            };
        }

        public IActionResult Match(Func<T, IActionResult> onSuccess, Func<List<Error>, IActionResult> onFailure)
        {
            if (IsSuccess)
            {
                return onSuccess(Value); // Retorna o resultado de onSuccess
            }
            else
            {
                return onFailure(ErrorClass); // Retorna o resultado de onFailure
            }
        }


    }

    public class Error
    {
        public string Message { get; set; }
        public ErrorType Type { get; set; }


        
    }

    public enum ErrorType
    {
        NotFound = 404,
        Validation = 422

    }

}

