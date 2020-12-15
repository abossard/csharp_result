using System;

namespace ConsoleApp1
{
    class ResultUselessHelpersIgnore
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

    
    
    public class Result<TD, TE>
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
       
        static Result<string, Exception> TryToCreateResultWithoutHelper()
        {
            var rand = new Random();
            return rand.NextDouble() >= 0.5 ? new("hi", null) : new(null, new Exception("Big freaking problem"));
        }
        
        static Result<bool, string> TryToCreateResultWithoutHelper2()
        {
            var rand = new Random();
            return rand.NextDouble() >= 0.5 ? new(true, null) : new(false, "Big freaking problem");
        }

        static void Main(string[] args)
        {
            var result = TryToCreateResultWithoutHelper();
            
            var message = result switch
            {
                {IsSuccess: true, Data: var data} => $"Got some: {data}",
                {IsSuccess: false, Error: var errorData} => $"Oops {errorData}",
            };

            var result2 = TryToCreateResultWithoutHelper2();
            
            var message2 = result2 switch
            {
                {IsSuccess: true, Data: var data} => $"Got some: {data}",
                {IsSuccess: false, Error: var errorData} => $"Oops {errorData}",
            };
            Console.WriteLine(message);
            Console.WriteLine(message2);
        }
    }
}