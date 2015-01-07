// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SummaryModuleSleepStoppedEventListener.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Modules
{
    using System.Linq;

    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Perseverance.Screens;
    using MetaMind.Perseverance.Sessions;

    public class SummaryModuleSleepStoppedEventListener : ListenerBase
    {
        public SummaryModuleSleepStoppedEventListener()
        {
            this.RegisteredEvents.Add((int)SessionEventType.SleepStopped);
        }

        public override bool HandleEvent(EventBase @event)
        {
            var screenManager = GameEngine.ScreenManager;

            var summary = screenManager.Screens.First(screen => screen is SummaryScreen);
            if (summary != null)
            {
                summary.ExitScreen();
            }

            screenManager.AddScreen(new MotivationScreen());

            return true;
        }
    }
}