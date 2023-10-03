namespace Shared.Exceptions;

public class QueryHandlerNotFoundException : Exception
{
    public QueryHandlerNotFoundException()
    {
    }

    public QueryHandlerNotFoundException(string message) : base(message)
    {
    }

    public QueryHandlerNotFoundException(Type type) : base(type.ToString())
    {
    }

    public QueryHandlerNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}