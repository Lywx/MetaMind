namespace MetaMind.Engine.Core.Entity.Common
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Provide sorting, filtering for MetaMind specific operations.
    /// </summary>
    public class MMEntityCollection<T> : IMMEntityCollection<T>
        where T : IMMEntity
    {
        private ObservableCollection<IMMBufferable> bufferCollection = new ObservableCollection<IMMBufferable>();

        private ObservableCollection<IMMDrawable> drawCollection = new ObservableCollection<IMMDrawable>();

        private ObservableCollection<IMMInputtable> inputCollection = new ObservableCollection<IMMInputtable>();

        /// <summary>
        /// Provides enumerators.
        /// </summary>
        private List<T> entityList = new List<T>();

        #region Constructors and Finalizer

        public MMEntityCollection()
        {
            this.Setup();
        }

        #endregion

        #region Event Handlers

        private void InputCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.inputCollection.Sort((a, b) => a.CompareTo(b));
        }

        private void DrawCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.drawCollection.Sort((a, b) => a.CompareTo(b));
        }

        private void BufferCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
        }

        #endregion

        #region Initialization

        private void InitializeHandlers()
        {
            this.bufferCollection.CollectionChanged += this.BufferCollection_CollectionChanged;
            this.drawCollection  .CollectionChanged += this.DrawCollection_CollectionChanged;
            this.inputCollection .CollectionChanged += this.InputCollection_CollectionChanged;
        }

        private void Setup()
        {
            this.InitializeHandlers();
        }

        #endregion

        #region MM Buffer

        public void UpdateBackwardBuffer()
        {
            foreach (var entity in this.bufferCollection)
            {
                entity.UpdateBackwardBuffer();
            }
        }

        public void UpdateForwardBuffer()
        {
            foreach (var entity in this.bufferCollection)
            {
                entity.UpdateForwardBuffer();
            }
        }

        #endregion

        #region MM Load and Unload

        public void LoadContent()
        {
            foreach (var entity in this)
            {
                entity.LoadContent();
            }
        }

        public void UnloadContent()
        {
            foreach (var entity in this)
            {
                entity.UnloadContent();
            }
        }

        #endregion

        #region MM Draw

        public void BeginDraw(GameTime time)
        {
            foreach (var entity in this.drawCollection)
            {
                entity.BeginDraw(time);
            }
        }

        public void Draw(GameTime time)
        {
            foreach (var entity in this.drawCollection.Where(entity => entity.EntityVisible))
            {
                entity.Draw(time);
            }
        }

        public void EndDraw(GameTime time)
        {
            foreach (var entity in this.drawCollection.Where(entity => entity.EntityVisible))
            {
                entity.EndDraw(time);
            }
        }

        #endregion

        #region MM Update

        public void Update(GameTime time)
        {
            foreach (var entity in this.Where(entity => entity.EntityEnabled))
            {
                entity.Update(time);
            }
        }

        public void UpdateInput(GameTime time)
        {
            foreach (var entity in this.inputCollection.Where(
                entity => entity.EntityEnabled &&
                          entity.EntityInputtable))
            {
                entity.UpdateInput(time);
            }
        }

        #endregion

        #region Collection Enumeration

        public IEnumerator<T> GetEnumerator()
        {
            return this.entityList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.entityList).GetEnumerator();
        }

        #endregion

        #region Collection Modification

        public void Add(T item)
        {
            if (this.entityList.Contains(item))
            {
                return;
            }

            if (item is IMMBufferable)
            {
                this.bufferCollection.Add(item as IMMBufferable);
            }

            if (item is IMMDrawable)
            {
                this.drawCollection.Add(item as IMMDrawable);
            }

            if (item is IMMInputtable)
            {
                this.inputCollection.Add(item as IMMInputtable);
            }

            this.entityList.Add(item);
        }

        public void Clear()
        {
            this.bufferCollection.Clear();
            this.drawCollection  .Clear();
            this.inputCollection .Clear();

            this.entityList.Clear();
        }

        public bool Remove(T item)
        {
            if (!this.entityList.Contains(item))
            {
                return false;
            }

            if (item is IMMBufferable)
            {
                this.bufferCollection.Remove(item as IMMBufferable);
            }

            if (item is IMMDrawable)
            {
                this.drawCollection.Remove(item as IMMDrawable);
            }

            if (item is IMMInputtable)
            {
                this.inputCollection.Remove(item as IMMInputtable);
            }

            return this.entityList.Remove(item);
        }

        #endregion

        #region Collection Mischievous

        public bool Contains(T item)
        {
            return this.entityList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.entityList.CopyTo(array, arrayIndex);
        }

        public int Count => this.entityList.Count;

        public bool IsReadOnly => ((ICollection<T>)this.entityList).IsReadOnly;

        #endregion
    }
}