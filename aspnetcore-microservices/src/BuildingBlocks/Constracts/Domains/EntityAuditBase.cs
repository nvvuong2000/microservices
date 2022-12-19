using Constracts.Domains.Interfaces;

namespace Constracts.Domains
{
    public abstract class EntityAuditBase<T> : EntityBase<T>, IAuditables
    {
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
    }
}
