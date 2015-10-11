﻿namespace MetaMind.Engine.Entities
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Collections;
    using Extensions;
    using Microsoft.Xna.Framework;

    public class MMEntityCollection<T> : ICollection<T>, IMMDrawOperations, IMMUpdateableOperations, IMMBufferOperations, IMMInteropOperations
        where T : IMMEntity
    {
        private ObservableCollection<IMMBufferable> bufferCollection = new ObservableCollection<IMMBufferable>();

        private ObservableCollection<IMMDrawable> drawCollection = new ObservableCollection<IMMDrawable>();

        private ObservableCollection<IMMInputable> inputCollection = new ObservableCollection<IMMInputable>();

        private List<T> list = new List<T>();

        public MMEntityCollection()
        {
            this.InitializeHandlers();
        }

        #region Initialization

        private void InitializeHandlers()
        {
            this.bufferCollection.CollectionChanged += this.BufferCollection_CollectionChanged;
            this.drawCollection  .CollectionChanged += this.DrawCollection_CollectionChanged;
            this.inputCollection .CollectionChanged += this.InputCollection_CollectionChanged;
        }

        #endregion

        #region Event Handlers

        private void InputCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
        }

        private void DrawCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // TODO: Wrong
            this.drawCollection.Sort((a, b) => -a.CompareTo(b));
        }

        private void BufferCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
        }

        #endregion

        #region Buffer

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

        #region Load and Unload

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

        #region Draw

        public void BeginDraw(GameTime time)
        {
            foreach (var entity in this.drawCollection)
            {
                entity.BeginDraw(time);
            }
        }

        public void Draw(GameTime time)
        {
            foreach (var entity in this.drawCollection.Where(entity => entity.Visible))
            {
                entity.Draw(time);
            }
        }

        public void EndDraw(GameTime time)
        {
            foreach (var entity in this.drawCollection.Where(entity => entity.Visible))
            {
                entity.EndDraw(time);
            }
        }

        #endregion

        #region Update

        public void Update(GameTime time)
        {
            foreach (var entity in this.Where(entity => entity.Enabled))
            {
                entity.Update(time);
            }
        }

        public void UpdateInput(GameTime time)
        {
            foreach (var entity in this.inputCollection.Where(
                entity => entity.Enabled &&
                          entity.Inputable))
            {
                entity.UpdateInput(time);
            }
        }

        #endregion

        #region ICollection

        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.list).GetEnumerator();
        }

        public void Add(T item)
        {
            if (item is IMMBufferable)
            {
                this.bufferCollection.Add(item as IMMBufferable);
            }

            if (item is IMMDrawable)
            {
                this.drawCollection.Add(item as IMMDrawable);
            }

            if (item is IMMInputable)
            {
                this.inputCollection.Add(item as IMMInputable);
            }

            this.list.Add(item);
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public bool Contains(T item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return this.list.Remove(item);
        }

        public int Count => this.list.Count;

        public bool IsReadOnly => ((ICollection<T>)this.list).IsReadOnly;

        #endregion
    }
}