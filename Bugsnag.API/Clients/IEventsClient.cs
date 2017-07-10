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
    public interface IEventsClient
    {
        Task<PaginatedResponse<IReadOnlyCollection<Event>>> ListEventsByProjectAsync(string projectId);
        Task<PaginatedResponse<IReadOnlyCollection<Event>>> ListEventsByErrorAsync(string projectId, string errorId);
        Task<Event> LatestEventByErrorAsync(string errorId);
        Task<Event> ShowAsync(string projectId, string eventId);
    }

    public class EventsClient : IEventsClient
    {
        public EventsClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        readonly IRestClient _restClient;

        public async Task<Event> LatestEventByErrorAsync(string errorId)
        {
            return await _restClient.SendAsync(
                method: HttpMethod.Get,
                relativePath: $"errors/{errorId}/latest_event",
                mutateRequestMessage: _ => { },
                deserializeResponseAsync: async response =>
                {
                    // TODO: Improve these error messages.
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    return Jil.JSON.Deserialize<Event>(json);
                });
        }

        public async Task<PaginatedResponse<IReadOnlyCollection<Event>>> ListEventsByErrorAsync(string projectId, string errorId)
        {
            return await _restClient.SendAsync(
                method: HttpMethod.Get,
                relativePath: $"projects/{projectId}/errors/{errorId}/events",
                mutateRequestMessage: _ => { },
                deserializeResponseAsync: async response =>
                {
                    // TODO: Improve these error messages.
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var events = Jil.JSON.Deserialize<IReadOnlyCollection<Event>>(json);

                    return new PaginatedResponse<IReadOnlyCollection<Event>>(result: events);
                });
        }

        public async Task<PaginatedResponse<IReadOnlyCollection<Event>>> ListEventsByProjectAsync(string projectId)
        {
            return await _restClient.SendAsync(
                method: HttpMethod.Get,
                relativePath: $"projects/{projectId}/events",
                mutateRequestMessage: _ => { },
                deserializeResponseAsync: async response =>
                {
                    // TODO: Improve these error messages.
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var events = Jil.JSON.Deserialize<IReadOnlyCollection<Event>>(json);

                    return new PaginatedResponse<IReadOnlyCollection<Event>>(result: events);
                });
        }

        public async Task<Event> ShowAsync(string projectId, string eventId)
        {
            return await _restClient.SendAsync(
                method: HttpMethod.Get,
                relativePath: $"projects/{projectId}/events/{eventId}",
                mutateRequestMessage: _ => { },
                deserializeResponseAsync: async response =>
                {
                    // TODO: Improve these error messages.
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    return Jil.JSON.Deserialize<Event>(json);
                });
        }
    }
}
