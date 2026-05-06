using System;
using System.Collections.Generic;
using System.Text;

// 新規ファイル: GenericTypeConstraintsPatterns/Entity/FileSourceToken.cs
using GenericTypeConstraintsPatterns.Interface;

namespace GenericTypeConstraintsPatterns.Entity
{
    public sealed record JsonSource(string FilePath) : IJsonSource;
    public sealed record CsvSource(string FilePath) : ICsvSource;
}