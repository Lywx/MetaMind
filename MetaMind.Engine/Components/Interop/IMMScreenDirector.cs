namespace MetaMind.Engine.Components.Interop
{
    using Screens;

    public interface IMMScreenDirector : IMMScreenDirectorOperations, IMMInputableComponent
    {
        #region Screen Data

        /// <summary>
        /// Expose an array holding all the screens. We return a copy rather
        /// than the real master list, because screens should only ever be added
        /// or removed using the AddScreen and RemoveScreen methods.
        /// </summary>
        IMMScreen[] Screens { get; }

        MMScreenSettings Settings { get; }

        #endregion

        #region Trace Data

        /// <summary>
        /// If true, the manager prints out a list of all the screens
        /// each time it is updated. This can be useful for making sure
        /// everything is being added and removed at the right times.
        /// </summary>
        bool TraceEnabled { get; set; }

        #endregion
    }
}