using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using gestaosala.core.models.agenda;
using gestaosala.core.providers.agenda;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace gestaosala.core.manager.agenda
{
    public class AgendaSalaManager : IAgendaSalaManager
    {
        #region Objects
        private readonly IAgendaSalaProvider _agendaSalaProvider;
        #endregion

        #region constructor
        public AgendaSalaManager(IAgendaSalaProvider agendaSalaProvider)
        {
            _agendaSalaProvider = agendaSalaProvider;
        }
        #endregion

        public Task<int> Delete(int salaId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<AgendaSalaModel>> GetAgendaSala()
        {
            throw new NotImplementedException();
        }

        public async Task<AgendaSalaModel> Insert(AgendaSalaModel agendaSala)
        {
            var response = await _agendaSalaProvider.Post(agendaSala);
            if (!response.IsSuccessStatusCode)
            {
                await ErrorResponse(response, "Post");
            }

            var json = await response.Content.ReadAsStringAsync();
            JObject jsonParse = JObject.Parse(json);
            var objectCliente = JsonConvert.DeserializeObject(jsonParse.ToString());

            var _sala = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<AgendaSalaModel>(objectCliente.ToString()));
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
