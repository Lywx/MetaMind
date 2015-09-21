namespace MetaMind.Engine.Component.Content.Asset
{
    using Microsoft.Xna.Framework.Content;

    public class AssetReader : ContentTypeReader<AssetXmlDocument>
    {
        /// <param name="input"></param>
        /// <param name="existingInstance">If is null, create a new xml document to load input</param>
        /// <returns></returns>
        protected override AssetXmlDocument Read(ContentReader input, AssetXmlDocument existingInstance)
        {
            if (existingInstance == null)
            {
                var doc = new AssetXmlDocument();
                doc.LoadXml(input.ReadString());

                return doc;
            }

            existingInstance.LoadXml(input.ReadString());
            return existingInstance;
        }
    }
}