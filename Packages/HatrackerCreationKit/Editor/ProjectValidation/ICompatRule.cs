namespace HatrackerCreationKit.Editor.ProjectValidation
{
    public interface ICompatRule
    {
        string Name { get; }
        CheckResult Check();
        bool CanFix { get; }
        void Fix();
    }

    public readonly struct CheckResult
    {
        public bool IsOk { get; }
        public string Expected { get; }
        public string Actual { get; }

        CheckResult(bool ok, string expected, string actual)
        {
            IsOk = ok;
            Expected = expected;
            Actual = actual;
        }

        public static CheckResult Ok() => new(true, null, null);
        public static CheckResult Mismatch(string expected, string actual) => new(false, expected, actual);
    }
}
