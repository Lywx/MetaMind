using System.Runtime.Serialization;
using MetaMind.Testimony.Concepts.Synchronizations;

namespace MetaMind.Acutance.Concepts
{
    public interface ICommand : ISynchronizable
    {
        #region File

        [DataMember]
        string Path { get; set; }

        #endregion

        #region Display

        [DataMember]
        string Name { get; set; }

        #endregion

        #region Operations

        void Reset();

        #endregion
    }
}