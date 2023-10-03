namespace Shared.Exceptions
{
    public class CommandHandlerNotFoundException : Exception
    {
        public CommandHandlerNotFoundException()
        {
        }

        public CommandHandlerNotFoundException(string message) : base(message)
        {
        }

        public CommandHandlerNotFoundException(Type type) : base(type.ToString())
        {
        }

        public CommandHandlerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}