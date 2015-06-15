namespace MetaMind.Engine.Collections
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    public class ObservableCollection<T> : System.Collections.ObjectModel.ObservableCollection<T>
    {
        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.Items.Add(item);
            }

            this.OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Reset));
        }

        public void RemoveRange(IEnumerable<T> collection)
        {
            foreach (var item in collection.Where(item => this.Items.Contains(item)).ToArray())
            {
                this.Items.Add(item);
            }

            this.OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Reset));
        }
    }
}