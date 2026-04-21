using System;
using System.Collections.Generic;

namespace HatrackerCreationKit.Editor.ProjectValidation
{
    public sealed class ValueRule<T> : ICompatRule
    {
        public string Name { get; }
        public bool CanFix => setter != null;

        readonly T expected;
        readonly Func<T> getter;
        readonly Action<T> setter;
        readonly IEqualityComparer<T> comparer;

        public ValueRule(string name, T expected, Func<T> getter, Action<T> setter = null, IEqualityComparer<T> comparer = null)
        {
            Name = name;
            this.expected = expected;
            this.getter = getter;
            this.setter = setter;
            this.comparer = comparer ?? EqualityComparer<T>.Default;
        }

        public ValueRule(string name, T expected, Func<T> getter, Action<T> setter, Func<T, T, bool> equals)
            : this(name, expected, getter, setter, new FuncEqualityComparer<T>(equals))
        {
        }

        sealed class FuncEqualityComparer<TValue> : IEqualityComparer<TValue>
        {
            readonly Func<TValue, TValue, bool> equals;
            public FuncEqualityComparer(Func<TValue, TValue, bool> equals) => this.equals = equals;
            public bool Equals(TValue x, TValue y) => equals(x, y);
            public int GetHashCode(TValue obj) => obj?.GetHashCode() ?? 0;
        }

        public CheckResult Check()
        {
            T actual;
            try
            {
                actual = getter();
            }
            catch (Exception e)
            {
                return CheckResult.Mismatch(expected?.ToString(), $"<error: {e.GetType().Name}>");
            }
            return comparer.Equals(actual, expected)
                ? CheckResult.Ok()
                : CheckResult.Mismatch(expected?.ToString(), actual?.ToString());
        }

        public void Fix()
        {
            if (setter == null) throw new InvalidOperationException($"{Name} is not fixable");
            setter(expected);
        }
    }
}
