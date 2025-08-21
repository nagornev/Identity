using Auth.Application.Abstractions.Services;
using Auth.Application.Options;
using Auth.Backgrounds.Abstractions.Processors;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Backgrounds.Processors
{
    public class DeleteInvalidPermissionsBackgroundProcessor : IDeleteInvalidPermissionsBackgroundProcessor
    {
        private readonly IDeleteInvalidPermissionsBackgroundService _deleteInvalidPermissionsBackgroundService;

        private readonly ApplicationOptions _applicationOptions;

        public DeleteInvalidPermissionsBackgroundProcessor(IDeleteInvalidPermissionsBackgroundService deleteInvalidPermissionsBackgroundService,
                                                           IOptions<ApplicationOptions> applicationOptions)
        {
            _applicationOptions = applicationOptions.Value;
            _deleteInvalidPermissionsBackgroundService = deleteInvalidPermissionsBackgroundService;
        }

        public async Task HandleAsync(CancellationToken cancellation)
        {
            await _deleteInvalidPermissionsBackgroundService.HandleAsync(cancellation);
            await Task.Delay(_applicationOptions.DeleteInvalidPermissionsDelay);
        }
    }
}
