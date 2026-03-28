namespace AHShows.Exceptions;

public class FolderNotValidAhSqDirectoryException: Exception
{
    private const string DefaultMessage = "De geselecteerde map is geen geldige Allen & Heath SQ map!";

    public FolderNotValidAhSqDirectoryException(): base(DefaultMessage)
    {
        
    }
}