namespace AHShows.Exceptions;

public class FolderProcessingIsBusyException: Exception
{
    private const string DefaultMessage = "Er wordt al een map verwerkt! Probeer het later nogmaals.";

    public FolderProcessingIsBusyException(): base(DefaultMessage)
    {
        
    }
}