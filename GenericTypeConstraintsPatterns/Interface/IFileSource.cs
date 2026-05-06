using System;
using System.Collections.Generic;
using System.Text;
// 新規ファイル: GenericTypeConstraintsPatterns/Interface/IFileSource.cs
namespace GenericTypeConstraintsPatterns.Interface
{
    public interface IJsonSource
    {
        string FilePath { get; }
    }

    public interface ICsvSource
    {
        string FilePath { get; }
    }
}
