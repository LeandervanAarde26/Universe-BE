namespace UniVerServer.Subjects.Exceptions;

public class LecturerIdMismatchException : Exception
{
    public LecturerIdMismatchException()
    {
    }

    public LecturerIdMismatchException(string message)
        : base(message)
    {
    }

    public LecturerIdMismatchException(string message, Exception inner)
        : base(message, inner)
    {
    }
}