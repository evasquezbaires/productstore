using System.Diagnostics.CodeAnalysis;

namespace ProductStore.Api.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public class DomainNotFoundException : DomainException
{
    public DomainNotFoundException(string message)
        : base(message)
    {
    }

    public DomainNotFoundException(string entity, Guid id)
        : base(EnrichMessage(entity, id))
    {
    }

    public DomainNotFoundException(string entity, string code)
    : base(EnrichMessageWithCode(entity, code))
    {
    }

    private static string EnrichMessage(string entity, Guid id)
    {
        return $"{entity} with Id: '{id}' does not exist.";
    }

    private static string EnrichMessageWithCode(string entity, string code)
    {
        return $"{entity} with code '{code}' does not exist.";
    }
}
