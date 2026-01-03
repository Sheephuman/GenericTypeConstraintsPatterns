using GenericTypeConstraintsPatterns.Interface;


namespace GenericTypeConstraintsPatterns.Entity
{

    /// <summary>
    /* 戻り値型を Interface 側で固定する必要がある

    IJsonReadable<LogEntity> により
    

LoadFromJson() の戻り値が LogEntity で確定
        */

    /// </summary>
        public sealed class LogEntity
           : IJsonReadable<LogEntity>,ILogItem
        {
            public DateTime Timestamp { get; init; } = DateTime.Now;
            public string Level { get; init; } = string.Empty;
            public string Message { get; init; } = string.Empty;

            public IEnumerable<LogEntity> LoadFromJson()
            {
                return new[]
                {
                new LogEntity
                {
                    Timestamp = DateTime.Now,
                    Level = "INFO",
                    Message = "Application started"
                }
            };
            }
        }

}
