using System;
using System.Runtime.Serialization;
using MetaMind.Engine;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Concepts.Cognitions
{
    [DataContract,
    KnownType( typeof( Consciousness ) ),
    KnownType( typeof( Synchronization ) )]
    public class Cognition : EngineObject
    {
        #region Components

        [DataMember] public Consciousness   Consciousness   { get; set; }
        [DataMember] public Synchronization Synchronization { get; set; }

        public bool Awake { get { return Consciousness.AwakeCondition; } }

        #endregion Components

        #region Constructors

        public Cognition()
        {
            Consciousness   = new ConsciousnessAwake();
            Synchronization = new Synchronization();
        }

        #endregion Constructors

        #region Update

        public void Update( GameTime gameTime )
        {
            Consciousness = Consciousness.Update( gameTime );
            Synchronization              .Update( gameTime );
        }

        #endregion Update

    }
}