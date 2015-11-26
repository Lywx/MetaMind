namespace MetaMind.Engine.Components.Input
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Xna.Framework.Input;
    using NLog;
    using Services.Loader;
    using Services.Parser.Elements;
    using Services.Parser.Grammar;
    using Sprache;

    public static class MMKeyboardBindingUtils
    {
#if DEBUG

        #region Logger

        private static readonly Logger logger =
            LogManager.GetCurrentClassLogger();

        #endregion

#endif

        public static MMKeyboardBinding<MMInputAction> Load<TActions>(IConfigurable loader)
            where TActions : MMInputActions
        {
            var actionBinding = new MMKeyboardBinding<MMInputAction>();

            foreach (var actionPair in ConfigurationLoader.LoadDuplicablePairs(loader))
            {
                LoadPair<TActions>(actionBinding, actionPair);
            }

            return actionBinding;
        }

        private static void LoadPair<TActions>(
            MMKeyboardBinding<MMInputAction> actionBinding,
            KeyValuePair<string, string> actionPair)
            where TActions : MMInputActions
        {
            var action = MMInputAction.None;

            // Call MMInputActions and its subclass' static TryParse(string, MMInputAction) method
            var parser = typeof(TActions).GetMethod("TryParse", BindingFlags.Static | BindingFlags.Public);

            var success = (bool)parser.Invoke(null, new object[] { actionPair.Key, action });
            if (success)
            {
                var sentence  = CommonGrammar.SentenceParser.Parse(actionPair.Value);
                var key       = GetKey(sentence);
                var modifiers = GetModifiers(sentence);

                actionBinding[action].Add(key, modifiers);
            }
        }

        /// <summary>
        ///     Parse mapping key from a sentence.
        /// </summary>
        private static Keys GetKey(Sentence sentence)
        {
            var keyName = sentence.Words[0];
            Keys key;

            if (Enum.TryParse(keyName, true, out key))
            {
                return key;
            }
#if DEBUG
            logger.Warn($"{keyName} is not a valid key.");
#endif
            return Keys.None;
        }

        /// <summary>
        ///     Parse mapping modifier from a sentence.
        /// </summary>
        /// <example>
        ///     Action0 = K alone
        ///     Action1 = K with LeftControl
        ///     Action2 = P with LeftControl and LeftShift
        /// </example>
        private static List<Keys> GetModifiers(Sentence sentence)
        {
            var modifiers = new List<Keys>();

            if (sentence.Words.Last() != "alone")
            {
                foreach (var modifierName in sentence.Words.Skip(2))
                {
                    if (modifierName == "and")
                    {
                        continue;
                    }

                    Keys modifier;
                    if (Enum.TryParse(modifierName, true, out modifier))
                    {
                        modifiers.Add(modifier);
                    }
                    else
                    {
#if DEBUG
                        logger.Warn($"{modifierName} is not a valid key.");
#endif
                    }
                }
            }

            return modifiers;
        }
    }
}
