using Application.Common.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class Result<T>
    {
        public HttpStatus HttpStatus { get; set; }
        public bool Succeeded { get; set; }
        public T Data { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public static Result<T> Success(T value)
        {
            return new Result<T>
            {
                Succeeded = true,
                HttpStatus = HttpStatus.OK,
                Data = value,
            };
        }

        public static Result<T> Success()
        {
            return new Result<T>
            {
                Succeeded = true,
                HttpStatus = HttpStatus.OK,
            };
        }

        public static Result<T> Failure(HttpStatus httpStatus, IEnumerable<string> errors)
        {
            return new Result<T>
            {
                Succeeded = false,
                HttpStatus = httpStatus,
                Errors = errors.Any()? errors : new List<string>()
            };
        }

        public static Result<T> Failure(IEnumerable<string> errors)
        {
            return new Result<T>
            {
                Succeeded = false,
                Errors = errors.Any() ? errors : new List<string>()
            };
        }

        public static Result<T> Failure(HttpStatus httpStatus, string error)
        {
            return new Result<T>
            {
                Succeeded = false,
                HttpStatus = httpStatus,
                Errors = new List<string> { error }
            };
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>
            {
                Succeeded = false,
                Errors = new List<string> { error }
            };
        }
    }
}