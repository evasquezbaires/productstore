using System.Diagnostics.CodeAnalysis;

namespace ProductStore.Api.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class DomainException : Exception
    {
        public DomainException()
        {
            Messages = new List<string>();
        }

        public DomainException(string message)
            : base(message)
        {
            Messages = new List<string>() { message };
        }

        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
            Messages = new List<string>() { message };
        }

        public List<string> Messages { get; private set; }
    }
}