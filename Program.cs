using System;

namespace ConsoleApp1
{
    class Result
    {
        public static Result<TD> Ok<TD>(TD data)
        {
            return new Result<TD>(data, true);
        }

        public static Result<TD> Failure<TD>(TD data)
        {
            return new Result<TD>(data, false);
        }
    }

    class Result<TD> : Result
    {
        public Result(TD data, bool isSuccess)
        {
            Data = data;
            IsSuccess = isSuccess;
        }

        public readonly bool IsSuccess;
        public readonly TD Data;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var success = Result.Ok("YES");
            var error = Result.Failure(new Exception());
            var message = success switch
            {
                {IsSuccess: true, Data: var data} => $"Got some: {data}",
                {IsSuccess: false, Data: var errorData} => $"Oops {errorData}",
            };

            Console.WriteLine(message);
        }
    }
}