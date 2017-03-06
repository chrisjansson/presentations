using System;

namespace Utils
{
    public abstract class Maybe<T>
    {
        public static Maybe<T> Some(T value) => new SomeImpl(value);

        public static Maybe<T> None { get; } = new NoneImpl();
    
        public abstract Maybe<U> Map<U>(Func<T, U> map);
    
        public abstract T DefaultIfNone(T def);

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
        }

        private class NoneImpl : Maybe<T>
        {
            public override Maybe<U> Map<U>(Func<T, U> map) => Maybe<U>.None;

            public override T DefaultIfNone(T value) => value;
        }
    }
}