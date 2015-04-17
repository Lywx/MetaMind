// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SummaryModuleSleepStoppedEventListener.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Modules
{
    using System.Linq;

    using MetaMind.Engine.Components.Events;
    using MetaMind.Perseverance.Screens;
    using MetaMind.Perseverance.Sessions;

    public class SummaryModuleSleepStoppedEventListener : Listener
    {
        public SummaryModuleSleepStoppedEventListener()
        {
            this.RegisteredEvents.Add((int)SessionEventType.SleepStopped);
        }

        public override bool HandleEvent(IEvent @event)
        {
            var screenManager = GameInterop.Screen;

            var summary = screenManager.Screens.First(screen => screen is SummaryScreen);
            if (summary != null)
            {
                summary.Exit();
            }

            screenManager.AddScreen(new MotivationScreen());

            return true;
        }
    }
}