using System;
using System.Collections.Generic;
using System.Globalization;
using MetaMind.Engine.Components.Messages;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Components
{
    public class MessageManager : EngineObject
    {
        #region Message Settings

        public MessageSettings Settings { get; set; }

        #endregion Message Settings

        #region Message Data

        private static MessageManager singleton;
        private List<FlashMessage> messages;

        #endregion Message Data

        #region Constructors

        public MessageManager( MessageSettings settings )
        {
            Settings = settings;

            ResetMessages();
        }

        #endregion Constructors

        #region Initialization

        public static MessageManager GetInstance( MessageSettings setting )
        {
            return singleton ?? ( singleton = new MessageManager( setting ) );
        }

        public void ResetMessages()
        {
            messages = new List<FlashMessage>();
        }

        #endregion Initialization

        #region Update and Draw

        public void Draw( GameTime gameTime )
        {
            if ( messages.Count == 0 )
                return;

            for ( var i = 0 ; i < messages.Count ; i++ )
            {
                var dm = messages[ i ];

                if ( string.IsNullOrEmpty( dm.CurrentMessage ) )
                    continue;

                dm.DrawnMessage += dm.CurrentMessage[ dm.CurrentIndex ].ToString( new CultureInfo( "en-US" ) );

                var messagePosition = new Vector2( dm.Position.X, dm.Position.Y + 30 * i );
                var messageColor = new Color( dm.FontColor.R - 100 + 15 * i, dm.FontColor.G - 100 + 15 * i, dm.FontColor.B - 100 + 15 * i, dm.FontColor.A - 100 + 50 * i );
                FontManager.DrawText( Settings.MessageFont, dm.DrawnMessage, messagePosition, messageColor, Settings.MessageSize );

                if ( dm.CurrentIndex == dm.CurrentMessage.Length - 1 )
                    continue;

                dm.CurrentIndex++;
                messages[ i ] = dm;
            }
        }

        public void Update( GameTime gameTime )
        {
            if ( messages.Count == 0 )
                return;

            for ( var i = 0 ; i < messages.Count ; i++ )
            {
                var dm = messages[ i ];
                dm.DisplayTime -= gameTime.ElapsedGameTime;
                if ( dm.DisplayTime <= TimeSpan.Zero )
                    messages.RemoveAt( i );
                else
                    messages[ i ] = dm;
            }
        }

        #endregion Update and Draw

        #region Operations

        public void PopMessages( string message, TimeSpan lastingPeriod, Vector2 postion, Color color )
        {
            messages.Add( new FlashMessage( message, lastingPeriod, postion, color ) );
        }

        public void PopMessages( string message, Vector2 postion, Color color )
        {
            messages.Add( new FlashMessage( message, Settings.MessageLastingPeriod, postion, color ) );
        }

        public void PopMessages( string message, Color color )
        {
            messages.Add( new FlashMessage( message, Settings.MessageLastingPeriod, Settings.MessagePosition, color ) );
        }

        public void PopMessages( string message )
        {
            messages.Add( new FlashMessage( message, Settings.MessageLastingPeriod, Settings.MessagePosition, Settings.MessageColor ) );
        }

        #endregion Operations
    }
}