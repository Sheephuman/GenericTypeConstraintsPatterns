using GenericTypeConstraintsPatterns.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericTypeConstraintsPatterns.Interface
{
    public interface IJsonLoader<TEntity>
    {
        IEnumerable<LogEntity> JsonLoad(string filePath);
    }

}
