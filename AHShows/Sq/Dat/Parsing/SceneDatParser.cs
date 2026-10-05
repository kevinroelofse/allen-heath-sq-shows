using System.Text.RegularExpressions;

namespace AHShows.Sq.Dat.Parsing;

public class SceneDatParser
{
    private static readonly Regex SceneFileNameRegex =
        new(
            @"^SCENE(?<number>\d{3})\.DAT$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public SqScene Parse(SqDatFile file)
    {
        ArgumentNullException.ThrowIfNull(file);

        Match match = SceneFileNameRegex.Match(file.FileName);

        if (!match.Success)
        {
            throw new ArgumentException(
                $"'{file.FileName}' is geen geldig SQ scene-bestand.",
                nameof(file));
        }

        int number = int.Parse(match.Groups["number"].Value);

        var scene = new SqScene
        {
            Number = number,
            FileName =  file.FileName,
        };
        // Hier komt de daadwerkelijke parsing van SCENExxx.DAT.
        scene.Name = Tools.Ascii.ReadNullTerminatedAscii(file.Data, 20);
        int startOffset = 884;
        for (int i = 0; i < 48; i++)
        {
            SqInput input = ParseInput(file.Data, i+1, startOffset + (i * 336));
            scene.Inputs.Add(input);
        } 

        return scene;
    }
    
    private SqInput ParseInput(byte[] data,int channelId, int offset)
    {
        SqInput input = new SqInput()
        {
            Number = channelId,
        };
        // naam uitlezen
        input.Name = Tools.Ascii.ReadAscii(data, offset, 6).TrimEnd('\0');
        
        // Kleur uitlezen
        int colorR = Convert.ToInt32(data[offset + 16]);
        int colorG = Convert.ToInt32(data[offset + 17]);
        int colorB = Convert.ToInt32(data[offset + 18]);
        input.Color = Color.FromArgb(colorR, colorG, colorB);
        
        // gain uitlezen
        
        // phantom power
        
        // polarity
        
        // routing
        
        // ...
        return input;
    }
}