using Auth.Domain.Exceptions.Domains.Scopes;
using Auth.Domain.ValueObjects;
using DDD.Primitives;
using Action = Auth.Domain.ValueObjects.Action;

namespace Auth.Domain.Aggregates
{
    public partial class Scope : AggregateRoot
    {
        private Scope(Guid id,
                      Action action,
                      Resource resource,
                      string description)
        {
            Id = id;
            Action = action;
            Resource = resource;
            Description = description;
        }

        /// <summary>
        /// Creates new scope.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="resource"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static Scope Create(string action,
                                   string resource,
                                   string description)
        {
            Guid id = Guid.NewGuid();

            Scope scope = new Scope(id,
                                    Action.Create(action),
                                    Resource.Create(resource),
                                    description);

            return scope;
        }

        public Action Action { get; private set; }

        public Resource Resource { get; private set; }

        public string Description { get; private set; }

        public string GetHash()
        {
            return $"{Action.Value}:{Resource.Value}";
        }

        /// <summary>
        /// Changes the scope action.
        /// </summary>
        /// <param name="action"></param>
        public void ChangeAction(string action)
        {
            Action = Action.Create(action) ??
                     throw new ActionNullDomainException();
        }

        /// <summary>
        /// Changes the scope resource.
        /// </summary>
        /// <param name="resource"></param>
        public void ChangeResource(string resource)
        {
            Resource = Resource.Create(resource) ??
                       throw new ResourceNullDomainException();
        }

        /// <summary>
        /// Changes the scope description.
        /// </summary>
        /// <param name="description"></param>
        public void ChangeDescription(string description)
        {
            Description = description;
        }
    }

    #region Ef

    public partial class Scope
    {
        private Scope()
        {
        }
    }

    #endregion
}
