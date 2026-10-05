using System.Text;

namespace AHShows.Tools;

public class Ascii
{
    
    internal static string ReadNullTerminatedAscii(byte[] buf, int start)
    {
        int idx = start;
        var sb = new StringBuilder();
        while (idx < buf.Length && buf[idx] != 0)
        {
            sb.Append((char)buf[idx]);
            idx++;
        }

        return sb.ToString();
    }
    
    internal static string ReadAscii(byte[] buf, int start, int length)
    {
        var sb = new StringBuilder();
        for (int i = start; i < start + length; i++)
        {
            sb.Append((char)buf[i]);
        }
        
        return sb.ToString();
    }
}