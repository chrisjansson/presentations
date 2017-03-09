using System;

namespace Utils
{
    public static class Maybe
    {
        public static Maybe<T> Some<T>(T value) => Maybe<T>.Some(value);
        public static Maybe<T> None<T>() => Maybe<T>.None;

        public static Maybe<T> ToMaybe<T>(this T value) where T : class
        {
            if (value == null)
            {
                return None<T>();
            }

            return Some(value);
        }

        public static Maybe<U> Map2<T1, T2, U>(Maybe<T1> m1, Maybe<T2> m2, Func<T1, T2, U> map2)
        {
            return m1
            .Map(a1 => m2
                .Map(a2 => map2(a1, a2)))
            .DefaultIfNone(Maybe.None<U>());
        }

        public static Result<TResult, TError> AsResult<TResult, TError>(this Maybe<TResult> source, TError errorIfNone)
        {
            return source
                .Map(x => Result<TResult, TError>.Ok(x))
                .DefaultIfNone(Result<TResult, TError>.Error(errorIfNone));
        }
    }

    public abstract class Maybe<T>
    {
        public static Maybe<T> Some(T value) => new SomeImpl(value);

        public static Maybe<T> None { get; } = new NoneImpl();

        public abstract Maybe<U> Map<U>(Func<T, U> map);

        public abstract T DefaultIfNone(T def);

        public abstract void Execute(Action<T> onSome, Action onError);

        private Maybe() { }

        private class SomeImpl : Maybe<T>
        {
            private readonly T _value;

            public SomeImpl(T value)
            {
                _value = value;
            }

            public override Maybe<U> Map<U>(Func<T, U> map) => Maybe<U>.Some(map(_value));

            public override T DefaultIfNone(T _) => _value;

            public override void Execute(Action<T> onSome, Action onError)
            {
                onSome(_value);
            }
        }

        private class NoneImpl : Maybe<T>
        {
            public override Maybe<U> Map<U>(Func<T, U> map) => Maybe<U>.None;

            public override T DefaultIfNone(T value) => value;

            public override void Execute(Action<T> onSome, Action onError)
            {
                onError();
            }
        }
    }
}