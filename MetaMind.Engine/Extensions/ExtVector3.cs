namespace Microsoft.Xna.Framework
{
    public static class ExtVector3
    {
        public static Color ToColor(this Vector3 vector3)
        {
            return new Color((int)vector3.X, (int)vector3.Y, (int)vector3.Z);
        }
    }
}