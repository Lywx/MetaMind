﻿#if WINDOWS
namespace MetaMind.Engine.Windows.Components.IO
{
    using Engine.Components.IO;

    public class MMPlatformPathWin32 : IMMPlatformPath
    {
        public string ConfigurationDirectory => @".\Configurations\";

        public string ContentDirectory       => @".\Content\";

        public string DataDirectory          => @".\Data\";

        public string SaveDirectory          => @".\Save\";
    }
}
#endif 