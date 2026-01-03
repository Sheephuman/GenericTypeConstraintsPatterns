using GenericTypeConstraintsPatterns.Entity;
using GenericTypeConstraintsPatterns.Interface;
using GenericTypeConstraintsPatterns.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericTypeConstraintsPatterns.Loader
{
    public sealed class JsonLoader<TEntity> : IJsonLoader<TEntity>
     where TEntity : IJsonReadable<TEntity>
    {
        public IEnumerable<LogEntity> JsonLoad(string filePath)
        {
            /// <summary>
            /// ラムダ式 はデリゲート型ではないため、'string' 型に変換できません →　シグネチャの不一致
            /// 対応： Func<string[], LogEntity> 型に変換されるようにJsonRepositoryにコンストラクタを追加
            /// 補足；JsonLoaderによるラップ関数のため、シグネチャを揃える必要がある
            /// 
            var jsonRepository =
            new JsonRepository<LogEntity>(filePath,
    _ =>
            {
                var logENtity = new LogEntity
                {
                    Timestamp = DateTime.Now,
                    Level = "INFO",
                    Message = "Application started"

                };
                return logENtity;
            });

            return jsonRepository.LoadAll();
        }

    }
}