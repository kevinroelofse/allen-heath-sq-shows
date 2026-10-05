namespace AHShows.Sq.Dat.Parsing;

public class ShowDatParser
{
    public SqShow Parse(SqDatFile file)
    {
        ArgumentNullException.ThrowIfNull(file);

        // TODO:
        // Hier komt de daadwerkelijke parsing van SHOW.DAT.

        var show = new SqShow();
        
        show.FolderName = GetFolderLastPathSegment(file.FilePath);
        show.Name = Tools.Ascii.ReadNullTerminatedAscii(file.Data, 0);
        
        return show;
    }

    private string GetFolderLastPathSegment(string path)
    {
        return new FileInfo(path).Directory!.Name;
    }
}