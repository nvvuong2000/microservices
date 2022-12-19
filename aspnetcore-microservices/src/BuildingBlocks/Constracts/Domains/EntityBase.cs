using Constracts.Domains.Interfaces;

namespace Constracts.Domains
{
    public abstract class EntityBase<TKey>: IEntityBase<TKey>
    {
        public TKey Id { get; set; }
    }
}
