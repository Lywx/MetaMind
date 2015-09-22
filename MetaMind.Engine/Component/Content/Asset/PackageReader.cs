namespace MetaMind.Engine.Component.Content.Asset
{
    using Microsoft.Xna.Framework.Content;

    public class PackageReader : ContentTypeReader<PackageXmlDocument>
    {
        /// <param name="input"></param>
        /// <param name="existingInstance">If is null, create a new xml document to load input</param>
        /// <returns></returns>
        protected override PackageXmlDocument Read(ContentReader input, PackageXmlDocument existingInstance)
        {
            if (existingInstance == null)
            {
                var doc = new PackageXmlDocument();
                doc.LoadXml(input.ReadString());

                return doc;
            }

            existingInstance.LoadXml(input.ReadString());
            return existingInstance;
        }
    }
}