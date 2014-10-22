using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Graphics;

namespace MetaMind.EngineProcessor
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    /// </summary>
    [ContentProcessor(DisplayName = "MetaMind.EngineProcessor.ChineseCharProcessor")]
    public class ChineseCharProcessor : FontDescriptionProcessor
    {
        public override SpriteFontContent Process(FontDescription input, ContentProcessorContext context)
        {
            var fullPath = Path.GetFullPath(CharsetFile);

            context.AddDependency(fullPath);

            var letters = File.ReadAllText(fullPath, System.Text.Encoding.UTF8);

            foreach (var c in letters)
            {
                input.Characters.Add(c);
            }

            return base.Process(input, context);
        }

        [DefaultValue( "Chinese5931.txt" )]
        [DisplayName("Chinese Character Set File")]
        [Description("The characters in this file will be automatically added to the font.")]
        public string CharsetFile
        {
            get { return charsetFile; }
        }

        private string charsetFile = @"..\MetaMind.EngineBuilderContent\Fonts\CharacterSets\Chinese5931.txt";
    }
}