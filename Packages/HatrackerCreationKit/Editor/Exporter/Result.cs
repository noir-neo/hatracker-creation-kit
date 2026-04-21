using System;

namespace HatrackerCreationKit.Editor.Exporter
{
    public abstract class Result<TValue>
    {
        public sealed class Success : Result<TValue>
        {
            public TValue Value { get; }

            public Success(TValue value)
            {
                Value = value;
            }

            public override string ToString() => $"Success({Value})";
        }

        public sealed class Failure : Result<TValue>
        {
            public Exception Error { get; }

            public Failure(Exception error)
            {
                Error = error;
            }

            public override string ToString() => $"Failure({Error})";
        }

        public static Result<TValue> FromValue(TValue value) => new Success(value);
        public static Result<TValue> FromException(Exception error) => new Failure(error);

    }
}