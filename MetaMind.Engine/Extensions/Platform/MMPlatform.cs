namespace MetaMind.Engine.Platform
{
    using System;
    using System.Reflection;

    public static class MMPlatform
    {
        #region Platform

        private static readonly Lazy<MMPlatformType> currentPlatform =
            new Lazy<MMPlatformType>(GetCurrentPlatform);

        public static MMPlatformType CurrentPlatform => currentPlatform.Value;

        private static MMPlatformType GetCurrentPlatform()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    return MMPlatformType.Win32;
                    break;

                case PlatformID.Unix:
                    return MMPlatformType.Linux;
                    break;

                case PlatformID.MacOSX:
                    return MMPlatformType.Mac;
                    break;

                default:
                    return MMPlatformType.Unknown;
                    break;
            }
        }

        #endregion

        #region Runtime

        private static readonly Lazy<string> currentRuntimeVersion =
            new Lazy<string>(GetCurrentRuntimeVersion);

        public static string CurrentRuntimeVersion
            => currentRuntimeVersion.Value;

        private static string GetCurrentRuntimeVersion()
        {
            var mono = Type.GetType("Mono.Runtime");
            if (mono == null)
            {
                return $".NET CLR {Environment.Version}";
            }

            // http://stackoverflow.com/questions/8413922/programmatically-determining-mono-runtime-version
            // http://stackoverflow.com/questions/4178129/how-to-determine-the-revision-from-which-current-mono-runtime-was-built-and-inst
            var version = mono.GetMethod(
                "GetDisplayName",
                BindingFlags.NonPublic | BindingFlags.Static);
            if (version == null)
            {
                return $"Mono (Unknown Version) CLR {Environment.Version}";
            }

            return
                $"Mono {version.Invoke(null, null)} CLR {Environment.Version}";
        }

        #endregion
    }
}