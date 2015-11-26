﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMind.Engine.Entities.Nodes
{
    public interface IMMNodeScheduleOperations
    {
        #region Schedule Update

        /// <summary>
        /// Schedules the 'Update' selector with a default priority. The
        /// 'Update' selector will be called every frame.
        /// </summary>
        void Schedule();

        /// <summary>
        /// Schedules the 'Update' selector with a default priority. The
        /// 'Update' selector will be called every frame.
        /// </summary>
        /// <param name="priority">The lower the priority, the earlier it is called.</param>
        void Schedule(int priority);

        #endregion

        #region Schedule Selector

        void Schedule(Action<float> selector);

        void Schedule(Action<float> selector, float interval);

        void ScheduleOnce(Action<float> selector, float delay);

        void Unschedule(Action<float> selector);

        #endregion

        void Unschedule();

        void UnscheduleAll();

        void Resume();

        void Pause();
    }
}
