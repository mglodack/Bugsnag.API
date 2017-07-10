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
    public interface IErrorsClient
    {
        Task<PaginatedResponse<IReadOnlyCollection<Error>>> ListErrorsAsync(string projectId);
    }

    public class ErrorsClient : IErrorsClient
    {
        public ErrorsClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        readonly IRestClient _restClient;

        public async Task<PaginatedResponse<IReadOnlyCollection<Error>>> ListErrorsAsync(string projectId)
        {
            return await _restClient
                .SendAsync(
                    method: HttpMethod.Get,
                    relativePath: "",
                    mutateRequestMessage: _ => { },
                    deserializeResponseAsync: async response =>
                    {
                        // TODO: Improve these error messages.
                        response.EnsureSuccessStatusCode();

                        var json = await response.Content.ReadAsStringAsync();
                        var errors = Jil.JSON.Deserialize<IReadOnlyCollection<Error>>(json);

                        return new PaginatedResponse<IReadOnlyCollection<Error>>(result: errors);
                    });
        }
    }
}
