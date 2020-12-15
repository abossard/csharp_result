using System;

namespace ConsoleApp1
{
    class Result
    {
        public static Result<TD, Exception> Ok<TD>(TD data)
        {
            return new(data, null);
        }

        public static Result<string, TE> FailureString<TE>(TE error) where TE : Exception
        {
            return new(null, error);
        }
    }

    
    
    class Result<TD, TE> : Result where TE: Exception
    {
        public Result(TD data, TE error)
        {
            Data = data;
            Error = error;
            IsSuccess = error == null;
        }

        public readonly bool IsSuccess;
        public readonly TD Data;
        public readonly TE Error;
    }

    

    class Program
    {
        static Result<string, Exception> TryToCreateResult()
        {
            var rand = new Random();
            return rand.NextDouble() >= 0.5 ? Result.Ok("Hi") : Result.FailureString(new Exception("Big freaking problem"));
        }
        
        static Result<string, Exception> TryToCreateResultWithoutHelper()
        {
            var rand = new Random();
            return rand.NextDouble() >= 0.5 ? new("hi", null) : new(null, new Exception("Big freaking problem"));
        }

        static void Main(string[] args)
        {
            var result = TryToCreateResultWithoutHelper();
            var message = result switch
            {
                {IsSuccess: true, Data: var data} => $"Got some: {data}",
                {IsSuccess: false, Error: var errorData} => $"Oops {errorData}",
            };

            Console.WriteLine(message);
        }
    }
}