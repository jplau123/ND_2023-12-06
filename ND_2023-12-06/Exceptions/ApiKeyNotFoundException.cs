namespace ND_2023_12_06.Exceptions;

public class ApiKeyNotFoundException : Exception
{
    public ApiKeyNotFoundException(string message) : base(message) { }
}
