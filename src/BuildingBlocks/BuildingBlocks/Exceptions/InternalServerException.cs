namespace BuildingBlocks.Exceptions;

public class InternalServerException : Exception
{
    public InternalServerException(string message) : base(message)
    {
        
    }

    public InternalServerException(string message, string details) : base(message)
    {
        Details = details ?? throw new ArgumentNullException(nameof(details));
    }
    
    public string Details { get; }
}