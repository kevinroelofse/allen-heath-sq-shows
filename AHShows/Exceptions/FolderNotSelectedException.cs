namespace AHShows.Exceptions;

public class FolderNotSelectedException: Exception
{
    private const string DefaultMessage = "Er is geen map geselecteerd!";
    
    public FolderNotSelectedException():base(DefaultMessage)
    {
        
    }
}