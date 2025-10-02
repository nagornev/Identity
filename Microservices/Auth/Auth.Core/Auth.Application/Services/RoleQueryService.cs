using Auth.Application.Abstractions.Services;
using Auth.Application.Exceptions.Applications.Roles;
using Auth.Domain.Aggregates;
using Auth.Domain.Specifications;
using DDD.Repositories;

namespace Auth.Application.Services
{
    public class RoleQueryService : IRoleQueryService
    {
        private readonly IRepositoryReader<Role> _roleRepository;

        public RoleQueryService(IRepositoryReader<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IReadOnlyCollection<Role>> GetRolesByIdsAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellation = default)
        {
            RoleByIdsSpecification specification = new RoleByIdsSpecification(ids);

            return await _roleRepository.FindAsync(specification, cancellation);
        }

        public async Task<Role> GetRoleByIdAsync(Guid id, CancellationToken cancellation = default)
        {
            RoleByIdSpecification roleIdSpecification = new RoleByIdSpecification(id);

            return await _roleRepository.GetAsync(roleIdSpecification, cancellation) ??
                   throw new RoleNotFoundApplicationException(id);
        }

        public async Task<Role> GetRoleByNameAsync(string name, CancellationToken cancellation = default)
        {
            RoleByNameSpecification roleNameSpecification = new RoleByNameSpecification(name);

            return await _roleRepository.GetAsync(roleNameSpecification, cancellation) ??
                   throw new RoleNotFoundApplicationException(name);
        }
    }
}
