using MetaMind.Engine.Components.Messages;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MetaMind.Engine.Components
{
    public class MessageManager : EngineObject
    {
        #region Singleton

        private static MessageManager singleton;

        public static MessageManager GetInstance()
        {
            return singleton ?? ( singleton = new MessageManager() );
        }

        #endregion Singleton

        #region Messages

        private List<FlashMessage> messages;

        #endregion Messages

        #region Constructors

        public MessageManager()
        {
            messages = new List<FlashMessage>();
        }

        #endregion Constructors

        #region Update and Draw

        public void Draw( GameTime gameTime )
        {
            if ( messages.Count == 0 )
                return;

            for ( var i = 0 ; i < messages.Count ; i++ )
            {
                var message = messages[ i ];

                if ( string.IsNullOrEmpty( message.CurrentMessage ) )
                    continue;

                message.DrawnMessage += message.CurrentMessage[ message.CurrentIndex ].ToString( new CultureInfo( "en-US" ) );

                var messagePosition = new Vector2( message.Position.X, message.Position.Y + 30 * i );
                var messageColor = new Color( message.FontColor.R - 100 + 15 * i, message.FontColor.G - 100 + 15 * i, message.FontColor.B - 100 + 15 * i, message.FontColor.A - 100 + 50 * i );

                FontManager.DrawText( MessageSettings.MessageFont, message.DrawnMessage, messagePosition, messageColor, MessageSettings.MessageSize );

                if ( message.CurrentIndex == message.CurrentMessage.Length - 1 )
                    continue;

                message.CurrentIndex++;
                messages[ i ] = message;
            }
        }

        public void Update( GameTime gameTime )
        {
            if ( messages.Count == 0 )
                return;

            for ( var i = 0 ; i < messages.Count ; i++ )
            {
                var message = messages[ i ];
                message.DisplayTime -= gameTime.ElapsedGameTime;
                if ( message.DisplayTime <= TimeSpan.Zero )
                    messages.RemoveAt( i );
                else
                    messages[ i ] = message;
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
            messages.Add( new FlashMessage( message, MessageSettings.MessageLastingPeriod, postion, color ) );
        }

        public void PopMessages( string message, Color color )
        {
            messages.Add( new FlashMessage( message, MessageSettings.MessageLastingPeriod, MessageSettings.MessagePosition, color ) );
        }

        public void PopMessages( string message )
        {
            messages.Add( new FlashMessage( message, MessageSettings.MessageLastingPeriod, MessageSettings.MessagePosition, MessageSettings.MessageColor ) );
        }

        #endregion Operations
    }
}