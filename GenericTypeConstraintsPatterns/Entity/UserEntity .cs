
using GenericTypeConstraintsPatterns.Interface;
using GenericTypeConstraintsPatterns.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GenericTypeConstraintsPatterns.Entity
{

    public class UserEntity : ICsvReadable<UserEntity>, IListItem
    {

        /// <summary>
        /// 'UserEntity' は、ジェネリック型またはメソッド 'ListViewModel<TEntity>' 内でパラメーター 'TEntity' として使用するために、パブリック パラメーターなしのコンストラクターを持つ非抽象型でなければなりません　　対応
        /// 引数なしのコンストラクターを追加することで、ListViewModel<UserEntity> の制約を満たすことができます。
        /// </summary>
        public UserEntity() { }  // 追加


        public UserEntity(int id, string displayName, string affiliation)
        {
            Id = id;
            DisplayName = displayName;
            this.affiliation = affiliation;
        }

        public int Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string affiliation { get; set; } = string.Empty;

        public ObservableCollection<UserEntity> LoadFromCsv()
        {
            throw new NotImplementedException();
        }
    }



}
