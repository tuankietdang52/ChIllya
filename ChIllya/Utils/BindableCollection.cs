using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ChIllya.Utils
{
    public class BindableCollection<T> : ObservableCollection<T>
    {
        public BindableCollection() : base()
        {

        }

        public BindableCollection(IEnumerable<T> collection) : base(collection)
        {

        }

        public BindableCollection(List<T> list) : base(list)
        {

        }

        public void AddRange(IEnumerable<T> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);
            CheckReentrancy();

            foreach (var item in collection)
            {
                Items.Add(item);
            }

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Reset));
        }

        public void ResetTo(IEnumerable<T> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);

            this.Items.Clear();
            AddRange(collection);
        }
    }
}
