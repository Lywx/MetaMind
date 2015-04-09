// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Message.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using MetaMind.Engine.Components.Messages;

    using Microsoft.Xna.Framework;

    public class MessageManager
    {
        #region Singleton

        private static MessageManager singleton;

        public static MessageManager GetInstance()
        {
            return singleton ?? (singleton = new MessageManager());
        }

        #endregion Singleton

        #region Messages

        private readonly List<FlashMessage> messages;

        #endregion Messages

        #region Constructors

        public MessageManager()
        {
            messages = new List<FlashMessage>();
        }

        #endregion Constructors

        #region Update and Draw

        public void Draw(GameTime gameTime)
        {
            if (messages.Count == 0)
            {
                return;
            }

            for (int i = 0; i < this.messages.Count; i++)
            {
                FlashMessage message = this.messages[i];

                if (string.IsNullOrEmpty(message.CurrentMessage))
                {
                    continue;
                }

                message.DrawnMessage += message.CurrentMessage[message.CurrentIndex].ToString(new CultureInfo("en-US"));

                var messagePosition = new Vector2(message.Position.X, message.Position.Y + 30 * i);
                var messageColor    = new Color(
                    message.FontColor.R - 100 + 15 * i, 
                    message.FontColor.G - 100 + 15 * i, 
                    message.FontColor.B - 100 + 15 * i, 
                    message.FontColor.A - 100 + 50 * i);

                GameEngine.FontManager.DrawString(
                    MessageSettings.MessageFont, 
                    message.DrawnMessage, 
                    messagePosition, 
                    messageColor, 
                    MessageSettings.MessageSize);

                if (message.CurrentIndex == message.CurrentMessage.Length - 1)
                {
                    continue;
                }

                message.CurrentIndex++;
                this.messages[i] = message;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (this.messages.Count == 0)
            {
                return;
            }

            for (int i = 0; i < this.messages.Count; i++)
            {
                var message = this.messages[i];
                message.DisplayTime -= gameTime.ElapsedGameTime;
                if (message.DisplayTime <= TimeSpan.Zero)
                {
                    this.messages.RemoveAt(i);
                }
                else
                {
                    this.messages[i] = message;
                }
            }
        }

        #endregion Update and Draw

        #region Operations

        public void PopMessages(string message, TimeSpan lastingPeriod, Vector2 postion, Color color)
        {
            messages.Add(new FlashMessage(message, lastingPeriod, postion, color));
        }

        public void PopMessages(string message, Vector2 postion, Color color)
        {
            messages.Add(new FlashMessage(message, MessageSettings.MessageLastingPeriod, postion, color));
        }

        public void PopMessages(string message, Color color)
        {
            messages.Add(
                new FlashMessage(message, MessageSettings.MessageLastingPeriod, MessageSettings.MessagePosition, color));
        }

        public void PopMessages(string message)
        {
            messages.Add(
                new FlashMessage(
                    message, 
                    MessageSettings.MessageLastingPeriod, 
                    MessageSettings.MessagePosition, 
                    MessageSettings.MessageColor));
        }

        #endregion Operations
    }
}