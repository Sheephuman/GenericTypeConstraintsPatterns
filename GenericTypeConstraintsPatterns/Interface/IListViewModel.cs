using System.Collections.ObjectModel;

namespace GenericTypeConstraintsPatterns.Interface
{
   public interface IListViewModel
    {
    

        Type CurrentEntityType { get; set; }

        ObservableCollection<object> Items { get; }
    }
}
