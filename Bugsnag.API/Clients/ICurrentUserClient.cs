using Bugsnag.API.Models;
using Restivus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.API.Clients
{
    public interface ICurrentUserClient
    {
        Task<PaginatedResponse<IReadOnlyCollection<Organization>>> ListOrganizationsAsync();
        Task<PaginatedResponse<IReadOnlyCollection<Project>>> ListProjects(string organizationId);
    }

    public class CurrentUserClient : ICurrentUserClient
    {
        public CurrentUserClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        readonly IRestClient _restClient;

        public async Task<PaginatedResponse<IReadOnlyCollection<Organization>>> ListOrganizationsAsync()
        {
            return await _restClient
                .SendAsync(
                    method: HttpMethod.Get,
                    relativePath: "user/organizations",
                    mutateRequestMessage: _ => { },
                    deserializeResponseAsync: async response =>
                    {
                        // TODO: Improve these error messages.
                        response.EnsureSuccessStatusCode();

                        var json = await response.Content.ReadAsStringAsync();
                        var organizations = Jil.JSON.Deserialize<IReadOnlyCollection<Organization>>(json);

                        return new PaginatedResponse<IReadOnlyCollection<Organization>>(result: organizations);
                    });
        }

        public async Task<PaginatedResponse<IReadOnlyCollection<Project>>> ListProjects(string organizationId)
        {
            return await _restClient
                .SendAsync(
                    method: HttpMethod.Get,
                    relativePath: $"/organizations/{organizationId}/projects",
                    mutateRequestMessage: _ => { },
                    deserializeResponseAsync: async response =>
                    {
                        // TODO: Improve these error messages.
                        response.EnsureSuccessStatusCode();

                        var json = await response.Content.ReadAsStringAsync();
                        var projects = Jil.JSON.Deserialize<IReadOnlyCollection<Project>>(json);

                        return new PaginatedResponse<IReadOnlyCollection<Project>>(result: projects);
                    });
        }
    }
}
