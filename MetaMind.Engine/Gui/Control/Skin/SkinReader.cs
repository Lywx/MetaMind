namespace MetaMind.Engine.Gui.Control.Skin
{
    using Microsoft.Xna.Framework.Content;

    public class SkinReader : ContentTypeReader<SkinXmlDocument>
    {
        /// <param name="input"></param>
        /// <param name="existingInstance">If is null, create a new xml document to load input</param>
        /// <returns></returns>
        protected override SkinXmlDocument Read(ContentReader input, SkinXmlDocument existingInstance)
        {
            if (existingInstance == null)
            {
                var doc = new SkinXmlDocument();
                doc.LoadXml(input.ReadString());

                return doc;
            }

            existingInstance.LoadXml(input.ReadString());
            return existingInstance;
        }
    }
}