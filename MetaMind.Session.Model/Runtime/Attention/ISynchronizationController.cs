namespace MetaMind.Session.Model.Runtime.Attention
{
    public interface ISynchronizationController
    {
        void BeginSync();

        void EndSync();

        void ToggleSync();
    }
}