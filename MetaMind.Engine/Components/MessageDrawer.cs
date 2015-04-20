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

    public class MessageDrawer : GameVisualEntity
    {
        #region Message Data

        private readonly List<FlashMessage> messages;

        public MessageSettings Settings { get; set; }

        #endregion

        #region Constructors

        public MessageDrawer(MessageSettings settings)
        {
            this.messages = new List<FlashMessage>();
            this.Settings = settings;
        }

        #endregion Constructors

        #region Update and Draw

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime, byte alpha)
        {
            if (this.messages.Count == 0)
            {
                return;
            }

            for (var i = 0; i < this.messages.Count; i++)
            {
                var message = this.messages[i];

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

                gameGraphics.StringDrawer.DrawString(
                    Settings.MessageFont, 
                    message.DrawnMessage, 
                    messagePosition, 
                    messageColor, 
                    Settings.MessageSize);

                if (message.CurrentIndex == message.CurrentMessage.Length - 1)
                {
                    continue;
                }

                message.CurrentIndex++;
                this.messages[i] = message;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (this.messages.Count == 0)
            {
                return;
            }

            for (var i = 0; i < this.messages.Count; i++)
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

        public void PopMessages(string message, TimeSpan life, Vector2 postion, Color color)
        {
            this.messages.Add(new FlashMessage(message, life, postion, color));
        }

        public void PopMessages(string message, Vector2 postion, Color color)
        {
            this.messages.Add(new FlashMessage(message, Settings.MessageLife, postion, color));
        }

        public void PopMessages(string message, Color color)
        {
            this.messages.Add(
                new FlashMessage(message, 
                    Settings.MessageLife,
                    Settings.MessagePosition, color));
        }

        public void PopMessages(string message)
        {
            this.messages.Add(
                new FlashMessage(message,
                    Settings.MessageLife,
                    Settings.MessagePosition,
                    Settings.MessageColor));
        }

        #endregion Operations
    }
}