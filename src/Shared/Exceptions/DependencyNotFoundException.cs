namespace Shared.Exceptions
{
    public class DependencyNotFoundException : Exception
    {
        public DependencyNotFoundException()
        {
        }

        public DependencyNotFoundException(string message) : base(message)
        {
        }

        public DependencyNotFoundException(Type type) : base(type.ToString())
        {
        }

        public DependencyNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}