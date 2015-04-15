namespace MetaMind.Engine.Components.Fonts
{
    public interface IStringProcessor
    {
        string CropMonospacedString(string str, float scale, int maxLength);

        string CropMonospacedStringByAsciiCount(string str, int count);

        string CropString(Font font, string str, float scale, int maxLength);

        string CropString(Font font, string str, float scale, int maxLength, bool monospaced);
    }
}