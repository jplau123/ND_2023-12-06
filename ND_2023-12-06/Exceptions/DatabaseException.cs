namespace ND_2023_12_06.Exceptions;

public class DatabaseException : Exception
{
    public DatabaseException(string message)
        : base(message)
    {
    }
}
