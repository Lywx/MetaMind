namespace MetaMind.Engine.Core.Backend.Audio
{
    using NLog;
    using Settings;

    public class MMAudioSettings: MMEnginePlainConfigurationRoot, IMMParameter 
    {
#if DEBUG
        #region Logger

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private int volumeEffect;

        private int volumeMaster;

        private int volumeMusic;

        #endregion

#endif

        public MMAudioSettings()
        {
            this.LoadConfiguration();
        }

        /// <summary>
        /// Sound effect volume.
        /// </summary>
        public int VolumeEffect
        {
            get { return this.volumeEffect; }
            set
            {
                if (this.IsValidVolume(value))
                {
                    this.volumeEffect = value;
                }
            }
        }

        /// <summary>
        /// Master volume govern other volumes.
        /// </summary>
        public int VolumeMaster
        {
            get { return this.volumeMaster; }
            set
            {
                if (this.IsValidVolume(value))
                {
                    this.volumeMaster = value;
                }
            }
        }

        /// <summary>
        /// Music volume.
        /// </summary>
        public int VolumeMusic
        {
            get { return this.volumeMusic; }
            set
            {
                if (this.IsValidVolume(value))
                {
                    this.volumeMusic = value;
                }
            }
        }

        #region Configuration

        private bool IsValidVolume(int volume)
        {
            if (100 < volume || volume < 0)
            {
                return false;
            }

            return true;
        }

        public void LoadConfiguration()
        {
            // TODO: Load from default and stored value in xml and prepare to serialize this class to store user settings

            //var configuration = MMPlainConfigurationLoader.LoadUnique(this);

            //this.VolumeEffect = MMPlainConfigurationReader.ReadValueInt(configuration, "Volume Effect", 100);
            //this.VolumeMaster = MMPlainConfigurationReader.ReadValueInt(configuration, "Volume Master", 100);
            //this.VolumeMusic  = MMPlainConfigurationReader.ReadValueInt(configuration, "Volume Music" , 100);
        }

        #endregion
    }
}