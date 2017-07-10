using Restivus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.API
{
    public class BugsnagRestClient : IRestClient
    {
        public BugsnagRestClient(IBugsnagCredentials bugsnagCredentials)
        {
            RequestMiddlewares = _ConfigureRequestMiddlewares(bugsnagCredentials);
        }

        public IWebApi WebApi { get; } = new BugsnagWebApi();

        public IReadOnlyCollection<IHttpRequestMiddleware> RequestMiddlewares { get; }

        public IHttpRequestSender RequestSender { get; } = new HttpRequestSender(new System.Net.Http.HttpClient());

        IReadOnlyCollection<IHttpRequestMiddleware> _ConfigureRequestMiddlewares(IBugsnagCredentials bugsnagCredentials) =>
            new List<IHttpRequestMiddleware>()
            {
                new HttpRequestMiddleware(message =>
                {
                    message.Headers.Add("Accept", "application/json");
                    message.Headers.Add("X-Version", "2");
                    message.Headers.Add("Authorization", $"token {bugsnagCredentials.Token}");
                }),
            };
    }

    public class BugsnagWebApi : IWebApi
    {
        public string Scheme { get; } = "https";

        public string Host { get; } = "api.bugsnag.com";

        public int? Port { get; } = 443;

        public string BasePath { get; } = "";
    }

    public interface IBugsnagCredentials
    {
        string Token { get; }
    }
}
