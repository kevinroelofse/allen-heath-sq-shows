using AHShows.Sq.Dat.Parsing;

namespace AHShows.Sq.Dat;

public sealed class SqDatReader
{
    private readonly ShowDatParser _showParser;
    private readonly SceneDatParser _sceneParser;

    public SqDatReader(
        ShowDatParser showParser,
        SceneDatParser sceneParser)
    {
        _showParser = showParser;
        _sceneParser = sceneParser;
    }

    public SqShow Read(string directory)
    {
        if (string.IsNullOrWhiteSpace(directory))
            throw new ArgumentException(
                "Directory mag niet leeg zijn.",
                nameof(directory));

        if (!Directory.Exists(directory))
            throw new DirectoryNotFoundException(directory);

        string showPath = Path.Combine(directory, "SHOW.DAT");

        if (!File.Exists(showPath))
            throw new FileNotFoundException(
                "SHOW.DAT niet gevonden.",
                showPath);

        // SHOW.DAT lezen
        SqDatFile showFile = ReadFile(showPath);

        SqShow show = _showParser.Parse(showFile);

        // SCENE000.DAT, SCENE001.DAT, etc.
        IEnumerable<string> sceneFiles =
            Directory.EnumerateFiles(
                directory,
                "SCENE*.DAT",
                SearchOption.TopDirectoryOnly);

        foreach (string scenePath in sceneFiles.OrderBy(GetSceneNumber))
        {
            SqDatFile sceneFile = ReadFile(scenePath);

            SqScene scene = _sceneParser.Parse(sceneFile);

            show.Scenes.Add(scene);
        }

        return show;
    }

    private static SqDatFile ReadFile(string path)
    {
        return new SqDatFile
        {
            FileName = Path.GetFileName(path),
            FilePath = path,
            Data = File.ReadAllBytes(path)
        };
    }

    private static int GetSceneNumber(string path)
    {
        string fileName = Path.GetFileNameWithoutExtension(path);

        if (fileName.Length < 8)
            return int.MaxValue;

        string number = fileName[5..];

        return int.TryParse(number, out int result)
            ? result
            : int.MaxValue;
    }
}