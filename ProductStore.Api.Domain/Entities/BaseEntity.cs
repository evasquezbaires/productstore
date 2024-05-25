using System.Diagnostics.CodeAnalysis;

namespace ProductStore.Api.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class BaseEntity
    {
        /// <summary>
        /// Unique Id to assign at the Entity
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Date when the new entity was created
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Last date when the entity was updated
        /// </summary>
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
