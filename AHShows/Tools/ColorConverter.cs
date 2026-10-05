namespace AHShows.Tools;

public class ColorConverter
{
    public static BasicColor ConvertColor(byte r, byte g, byte b)
    {
        int value = (r > 0 ? 1 : 0)
                    | (g > 0 ? 2 : 0)
                    | (b > 0 ? 4 : 0);

        return value switch
        {
            0 => BasicColor.Black,
            1 => BasicColor.Red,
            2 => BasicColor.Green,
            3 => BasicColor.Yellow,
            4 => BasicColor.Blue,
            5 => BasicColor.Magenta,
            6 => BasicColor.Cyan,
            7 => BasicColor.White,
            _ => throw new ArgumentException("Ongeldige RGB-waarde")
        };
    }
    
    public static BasicColor ConvertColor(Color color)
    {
        return ConvertColor(color.R, color.G, color.B);
    }
}