namespace AHShows.Sq.Dat;

public class SqShow
{
    public string? Name { get; set; }
    public string? FolderName { get; set; }

    public List<SqScene> Scenes { get; } = [];
    public List<SqPreAmp> Inputs { get; set; } = [];
}