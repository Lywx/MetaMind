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

    public class MessageManager : DrawableGameComponent
    {
        #region Singleton

        private static MessageManager Singleton { get; set; }

        public static MessageManager GetInstance(GameEngine gameEngine)
        {
            return Singleton ?? (Singleton = new MessageManager(gameEngine));
        }

        #endregion Singleton

        #region Message Data

        private readonly List<FlashMessage> messages;

        #endregion

        #region Engine Data

        private IGameGraphics GameGraphics { get; set; }

        #endregion

        #region Constructors

        private MessageManager(GameEngine gameEngine)
            : base(gameEngine)
        {
            this.GameGraphics = new GameEngineGraphics(gameEngine);

            this.messages = new List<FlashMessage>();
        }

        #endregion Constructors

        #region Update and Draw

        public override void Draw(GameTime gameTime)
        {
            if (this.messages.Count == 0)
            {
                return;
            }

            for (int i = 0; i < this.messages.Count; i++)
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

                this.GameGraphics.Font.DrawString(
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

        public void PopMessages(string message, TimeSpan lastingPeriod, Vector2 postion, Color color)
        {
            this.messages.Add(new FlashMessage(message, lastingPeriod, postion, color));
        }

        public void PopMessages(string message, Vector2 postion, Color color)
        {
            this.messages.Add(new FlashMessage(message, MessageSettings.MessageLastingPeriod, postion, color));
        }

        public void PopMessages(string message, Color color)
        {
            this.messages.Add(new FlashMessage(message, MessageSettings.MessageLastingPeriod, MessageSettings.MessagePosition, color));
        }

        public void PopMessages(string message)
        {
            this.messages.Add(
                new FlashMessage(
                    message,
                    MessageSettings.MessageLastingPeriod,
                    MessageSettings.MessagePosition,
                    MessageSettings.MessageColor));
        }

        #endregion Operations
    }
}