using gestaosala.core.helpers;
using gestaosala.core.models.agenda;
using gestaosala.core.providers.agenda;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace gestaosala.provider.agenda
{
    public class AgendaSalaProvider : IAgendaSalaProvider
    {
        #region Objects

        private readonly HttpClient _client;

        #endregion

        #region constructor
        public AgendaSalaProvider()
        {
            _client = new HttpClient();
            _client.BaseAddress = core.AppSettings.Apis.AgendaSala;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Task<HttpResponseMessage> Delete(int salaId)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> GetAgendaSala() =>
        await new HttpClientHelper(_client)
           .SetEndpoint($"Get")
           .GetAsync();
        #endregion

        #region Post
        public async Task<HttpResponseMessage> Post(AgendaSalaModel agendasala) =>
         await new HttpClientHelper(_client)
            .SetEndpoint($"Post")
            .WithContentSerialized(agendasala)
            .PostAsync();


        #endregion
    }
}
