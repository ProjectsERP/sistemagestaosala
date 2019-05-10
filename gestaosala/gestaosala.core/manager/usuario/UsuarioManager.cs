using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using gestaosala.core.models.usuario;
using gestaosala.core.providers.usuario;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace gestaosala.core.manager.usuario
{
    public class UsuarioManager : IUsuarioManager
    {
        #region Objects
        private readonly IUsuarioProvider _usuarioProvider;
        #endregion

        #region constructor
        public UsuarioManager(IUsuarioProvider usuarioProvider)
        {
            _usuarioProvider = usuarioProvider;
        }
        #endregion

        public async Task<UsuarioModel> Insert(UsuarioModel usuario)
        {
            var response = await _usuarioProvider.Insert(usuario);
            if (!response.IsSuccessStatusCode)
            {
                await ErrorResponse(response, "Post");
            }

            var json = await response.Content.ReadAsStringAsync();
            JObject jsonParse = JObject.Parse(json);
            var objectCliente = JsonConvert.DeserializeObject(jsonParse.ToString());

            var user = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<UsuarioModel>(objectCliente.ToString()));
            return user;
        }

        #region Metodos Auxiliares
        private async Task ErrorResponse(HttpResponseMessage httpResponseMessage, string log)
        {
            var contentResult = await httpResponseMessage.Content.ReadAsStringAsync();

            // Logger.LogError($"{ log}: API Returned Error :( - {httpResponseMessage.StatusCode}-{httpResponseMessage.ReasonPhrase} -> {contentResult}");

            if (((int)httpResponseMessage.StatusCode) >= 400 && ((int)httpResponseMessage.StatusCode) < 500)
                throw new Exception(contentResult);

            throw new Exception("Oops! Ocorreu um erro inesperado. Por favor, tente novamente.");
        }
        #endregion
    }
}
