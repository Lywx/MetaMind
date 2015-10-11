namespace MetaMind.Engine.Entities.Nodes
{
    /// <summary>
    /// Update interface in CocosSharp. I decided to use it alongside
    /// with IMMUpdateable to enable more flexibility.
    /// </summary>
    /// <param name="dt">
    /// Delta time in seconds. 
    /// </param>
    public interface ICCUpdatable
    {
        void Update(float dt);
    }
}
