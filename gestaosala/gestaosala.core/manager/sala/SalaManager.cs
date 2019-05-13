using gestaosala.core.models.sala;
using gestaosala.core.providers.salas;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace gestaosala.core.manager.sala
{
    public class SalaManager : ISalaManager
    {

        #region Objects
        private readonly ISalaProvider _salaProvider;
        #endregion

        #region constructor
        public SalaManager(ISalaProvider salaProvider)
        {
            _salaProvider = salaProvider;
        }

        public async Task<int> Delete(int salaId)
        {
            var response = await _salaProvider.Delete(salaId);
            if (!response.IsSuccessStatusCode)
            {
                await ErrorResponse(response, "Delete");
            }

            var json = await response.Content.ReadAsStringAsync();
            return Convert.ToInt32(json);

        }

        public async Task<IList<SalaModel>> GetSalas()
        {
            var response = await _salaProvider.GetSalas();
            if (!response.IsSuccessStatusCode)
            {
                await ErrorResponse(response, "Get");
            }

            var json = await response.Content.ReadAsStringAsync();
            JArray jsonArray = JArray.Parse(await response.Content.ReadAsStringAsync());

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<IList<SalaModel>>(jsonArray.ToString().TrimStart('{').TrimEnd('}')));
         
        }
        #endregion

        public async Task<SalaModel> Post(SalaModel sala)
        {
            var response = await _salaProvider.Post(sala);
            if (!response.IsSuccessStatusCode)
            {
                await ErrorResponse(response, "Post");
            }

            var json = await response.Content.ReadAsStringAsync();
            JObject jsonParse = JObject.Parse(json);
            var objectCliente = JsonConvert.DeserializeObject(jsonParse.ToString());

            var _sala = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<SalaModel>(objectCliente.ToString()));
            return _sala;
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
