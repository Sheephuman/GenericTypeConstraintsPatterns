using GenericTypeConstraintsPatterns.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GenericTypeConstraintsPatterns.Interface
{

    /// <summary>
    /*自己型制約付き ICsvReadable<TEntity> は

「静的メソッドの戻り値を型安全に固定したい」場合に有効

デメリットは、読者が CRTP を理解する必要があること

つまり「やや上級者向け」だが、Where:T の威力を見せるには最適
    */
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ICsvReadable<TEntity>
      where TEntity : ICsvReadable<TEntity>  /// 自己型制約
        /*「TEntity は ICsvReadable<TEntity> を実装している必要がある」
            実際には TEntity 自身がこのインターフェイスを実装するのが前提 */
    {
        // CSV から自分自身のコレクションを生成する静的メソッド
        abstract ObservableCollection<TEntity> LoadFromCsv();
    }



    /*
     
    public interface ICsvReadable だと
    
    効果：型パラメータなしで、単純に「CSV から生成できる」型であることを表現

    ・反作用
    戻り値の型が object 系統になりやすく、型安全性が低下
    ViewModel などで「TEntity に対応した CSV」を扱う場合は型キャストが必要
     */
}
