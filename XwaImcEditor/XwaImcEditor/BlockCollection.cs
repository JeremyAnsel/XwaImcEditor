using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace XwaImcEditor
{
    public class BlockCollection : ObservableCollection<Block>
    {
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);

            this.UnregisterPropertyChanged(e.OldItems);
            this.RegisterPropertyChanged(e.NewItems);

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Reset:
                    this.Sort();
                    break;
            }
        }

        protected override void ClearItems()
        {
            this.UnregisterPropertyChanged(this.Items);

            base.ClearItems();
        }

        private void RegisterPropertyChanged(IEnumerable items)
        {
            if (items != null)
            {
                foreach (var item in items.OfType<INotifyPropertyChanged>())
                {
                    item.PropertyChanged += OnItemPropertyChanged;
                }
            }
        }

        private void UnregisterPropertyChanged(IEnumerable items)
        {
            if (items != null)
            {
                foreach (var item in items.OfType<INotifyPropertyChanged>())
                {
                    item.PropertyChanged -= OnItemPropertyChanged;
                }
            }
        }

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Sort();
        }

        private void Sort()
        {
            var items = this.OrderBy(t => t.Position).ToList();

            foreach (var item in items)
            {
                this.Move(this.IndexOf(item), items.IndexOf(item));
            }
        }
    }
}
