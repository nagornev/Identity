using Auth.Domain.Exceptions.Domains.Scopes;
using Auth.Domain.ValueObjects;
using DDD.Primitives;
using Action = Auth.Domain.ValueObjects.Action;

namespace Auth.Domain.Aggregates
{
    public partial class Scope : AggregateRoot
    {
        private Scope(Guid id,
                      Audience audience,
                      Action action,
                      Resource resource,
                      string description)
        {
            Id = id;
            Audience = audience;
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
        public static Scope Create(string audience,
                                   string action,
                                   string resource,
                                   string description)
        {
            Guid id = Guid.NewGuid();

            Scope scope = new Scope(id,
                                    Audience.Create(audience),
                                    Action.Create(action),
                                    Resource.Create(resource),
                                    description);

            return scope;
        }

        public Audience Audience { get; private set; }

        public Action Action { get; private set; }

        public Resource Resource { get; private set; }

        public string Description { get; private set; }

        public string GetName()
        {
            return $"{Action.Value}:{Resource.Value}";
        }

        /// <summary>
        /// Change the scope audience.
        /// </summary>
        /// <param name="audience"></param>
        /// <exception cref="AudienceNullDomainException"></exception>
        public void ChangeAudience(string audience)
        {
            Audience = Audience.Create(audience) ??
                       throw new AudienceNullDomainException();
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
