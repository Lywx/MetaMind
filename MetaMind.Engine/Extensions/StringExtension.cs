using System.Text;

namespace MetaMind.Engine.Extensions
{
    public static class StringExtension
    {
        public static bool IsAscii( this string  value )
        {
            // ASCII encoding replaces non-ascii with question marks, so we use UTF8 to see if multi-byte sequences are there
            return Encoding.UTF8.GetByteCount( value ) == value.Length;
        }
    }
}