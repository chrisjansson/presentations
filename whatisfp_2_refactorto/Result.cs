using System;

namespace Utils
{
    public abstract class Result<TResult, TError>
    {
        public static Result<TResult, TError> Ok(TResult result)
        {
            return new OkImpl(result);
        }

        public static Result<TResult, TError> Error(TError error)
        {
            return new ErrorImpl(error);
        }

        public abstract Result<TOut, TError> Map<TOut>(Func<TResult, TOut> map);

        private class OkImpl : Result<TResult, TError>
        {
            private readonly TResult _value;

            public OkImpl(TResult value)
            {
                _value = value;
            }
        
            public override Result<TOut, TError> Map<TOut>(Func<TResult, TOut> map)
            {
                var result = map(_value);
                return Result<TOut, TError>.Ok(result);
            }
        }

        private class ErrorImpl : Result<TResult, TError>
        {
            private readonly TError _error;

            public ErrorImpl(TError error)
            {
                _error = error;
            }

            public override Result<TOut, TError> Map<TOut>(Func<TResult, TOut> map)
            {
                return Result<TOut, TError>.Error(_error);
            }
        }
    }
}