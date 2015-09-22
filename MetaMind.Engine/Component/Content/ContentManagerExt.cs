namespace MetaMind.Engine.Component.Content
{
    using System;
    using System.Threading;
    using Microsoft.Xna.Framework.Content;

    public static class ContentManagerExt
    {
        public static void LoadAsync<T>(this ContentManager content, string assetName, Action<T> assetAction)
        {
            ThreadPool.QueueUserWorkItem(
                callback =>
                {
                    var asset = content.Load<T>(assetName);

                    assetAction?.BeginInvoke(asset, null, null);
                });
        }
    }
}
