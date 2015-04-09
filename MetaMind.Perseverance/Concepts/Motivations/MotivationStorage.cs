// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Motivation.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Concepts.Motivations
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class MotivationStorage
    {
        public MotivationStorage()
        {
            this.MotivationLists = new List<MotivationList>();
        }

        [DataMember]
        public List<MotivationList> MotivationLists { get; private set; }

        public Motivation Create(int id)
        {
            if (this.Exist(id))
            {
                var entry = new Motivation();

                this.MotivationLists[id].Add(entry);

                return entry;
            }

            return null;
        }

        public void Remove(Motivation entry, int id)
        {
            if (this.Exist(id))
            {
                this.MotivationLists[id].Remove(entry);
            }
            else
            {
                throw new IndexOutOfRangeException("id");
            }
        }

        private bool Exist(int id)
        {
            return id < this.MotivationLists.Count;
        }
    }
}