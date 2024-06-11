namespace UniVerServer.Exceptions;

public class NotFoundException : Exception
{
    public Guid RequestedUserId { get; }
    public string Email { get; }
    public string CustomMessage { get; set; }

    public NotFoundException(Guid requestedUserId) : base($"User with ID: {requestedUserId} not found.")
    {
        RequestedUserId = requestedUserId;
    }

    public NotFoundException(string customMessage) : base(customMessage)
    {
        CustomMessage = customMessage;
    }
}