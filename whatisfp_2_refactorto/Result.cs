using System;

namespace Utils
{
    public static class Result
    {
        public static Result<TR, TE> Bind2<T1, T2, TR, TE>(
            Result<T1, TE> r1,
            Result<T2, TE> r2,
            Func<T1, T2, Result<TR, TE>> f)
        {
            return r1.Map(a1 => r2.Map(a2 => f(a1, a2)))
                .Fold<Result<TR, TE>>(
                    r => r.Fold(
                        rInner => rInner, 
                        e => Result<TR, TE>.Error(e)), 
                    e => Result<TR, TE>.Error(e));
        }
    }

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

        public abstract void Execute(Action<TResult> onOk, Action<TError> onError);

        public abstract TOut Fold<TOut>(Func<TResult, TOut> onOk, Func<TError, TOut> onError);

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

            public override void Execute(Action<TResult> onOk, Action<TError> onError)
            {
                onOk(_value);
            }

            public override TOut Fold<TOut>(Func<TResult, TOut> onOk, Func<TError, TOut> onError)
            {
                return onOk(_value);
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

            public override void Execute(Action<TResult> onOk, Action<TError> onError)
            {
                onError(_error);
            }

            public override TOut Fold<TOut>(Func<TResult, TOut> onOk, Func<TError, TOut> onError)
            {
                return onError(_value);
            }
        }
    }
}