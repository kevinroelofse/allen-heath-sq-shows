namespace AHShows.Exceptions;

public class FolderDoesNotExistsException: Exception
{
    private const string DefaultMessage = "De geselecteerde map bestaat niet!";

    public FolderDoesNotExistsException(): base(DefaultMessage)
    {
        
    }
}