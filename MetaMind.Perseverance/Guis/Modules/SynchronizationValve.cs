namespace MetaMind.Perseverance.Guis.Modules
{
    public class SynchronizationValve
    {
        public bool Opened { get; private set; }

        public void Close()
        {
            this.Opened = false;
        }

        public void Flip()
        {
            if (this.Opened)
            {
                this.Close();
            }
            else
            {
                this.Open();
            }
        }

        public void Open()
        {
            this.Opened = true;
        }
    }
}