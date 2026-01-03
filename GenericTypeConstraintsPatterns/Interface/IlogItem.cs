

namespace GenericTypeConstraintsPatterns.Interface
{
    public interface ILogItem
    {
        public DateTime Timestamp { get; init; }
        public string Level { get; init; }
        public string Message { get; init; }
    }
}
