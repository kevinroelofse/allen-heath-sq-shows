namespace AHShows.Sq.Dat;

public class SqScene
{
    public int Number { get; init; }

    public string? FileName { get; set; }
    public string? Name { get; set; }
    public List<SqInput> Inputs { get; } = [];
}