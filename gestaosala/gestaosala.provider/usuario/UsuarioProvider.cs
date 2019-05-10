using gestaosala.core.helpers;
using gestaosala.core.models.usuario;
using gestaosala.core.providers.usuario;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace gestaosala.provider.usuario
{
    public class UsuarioProvider : IUsuarioProvider
    {

        #region Objects

        private readonly HttpClient _client;

        #endregion

        #region constructor
        public UsuarioProvider()
        {
            _client = new HttpClient();
            _client.BaseAddress = core.AppSettings.Apis.Usuario;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        #region Insert
        public async Task<HttpResponseMessage> Insert(UsuarioModel usuario) =>
         await new HttpClientHelper(_client)
            .SetEndpoint($"Usuario")
            .WithContentSerialized(usuario)
            .PostAsync();
        #endregion
    }
}
