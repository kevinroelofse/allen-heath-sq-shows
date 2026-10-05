namespace AHShows.Sq.Dat;

public class SqDatFile
{
    public required string FileName { get; init; }

    public required string FilePath { get; init; }

    public required byte[] Data { get; init; }

    public int Length => Data.Length;
}