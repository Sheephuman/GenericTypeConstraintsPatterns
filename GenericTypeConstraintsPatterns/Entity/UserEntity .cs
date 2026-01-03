
using GenericTypeConstraintsPatterns.Interface;
using GenericTypeConstraintsPatterns.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GenericTypeConstraintsPatterns.Entity
{

    public class UserEntity : ICsvReadable<LogEntity>, IListItem
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string affiliation { get; set; } = string.Empty;

        public ObservableCollection<UserEntity> LoadFromCsv()
        {
            throw new NotImplementedException();
        }
    }



}
