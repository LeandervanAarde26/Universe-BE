namespace UniVerServer.Exceptions;

public class NotFoundException : Exception
{
    public Guid RequestedUserId { get; }
    public string Email { get; }

    public NotFoundException(Guid requestedUserId) : base($"User with ID: {requestedUserId} not found.")
    {
        RequestedUserId = requestedUserId;
    }
}