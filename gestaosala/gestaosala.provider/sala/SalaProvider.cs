using gestaosala.core.helpers;
using gestaosala.core.models.sala;
using gestaosala.core.providers.salas;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace gestaosala.provider.sala
{
    public class SalaProvider : ISalaProvider
    {
        #region Objects

        private readonly HttpClient _client;

        #endregion

        #region constructor
        public SalaProvider()
        {
            _client = new HttpClient();
            _client.BaseAddress = core.AppSettings.Apis.Sala;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        #region Get
        public async Task<HttpResponseMessage> GetSalas() =>
         await new HttpClientHelper(_client)
            .SetEndpoint($"Get")
            .GetAsync();
        #endregion
    
        #region Post
        public async Task<HttpResponseMessage> Post(SalaModel sala) =>
         await new HttpClientHelper(_client)
            .SetEndpoint($"Post")
            .WithContentSerialized(sala)
            .PostAsync();
        #endregion

        #region Delete
        public async Task<HttpResponseMessage> Delete(int salaId) =>
         await new HttpClientHelper(_client)
            .SetEndpoint($"Delete/{salaId}")
            .DeleteAsync();

        //  .SetEndpoint($"get/{estadoId.ToString()}")
        #endregion
    }
}
