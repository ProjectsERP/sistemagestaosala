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

        public async Task<int> Delete(int agendamentoId, int salaId)
        {
            var response = await _agendaSalaProvider.Delete(agendamentoId, salaId);
            if (!response.IsSuccessStatusCode)
            {
                await ErrorResponse(response, "Delete");
            }

            var json = await response.Content.ReadAsStringAsync();
            return Convert.ToInt32(json);
        }

        public async Task<IList<AgendaSalaModel>> GetAgendaSala()
        {
            var response = await _agendaSalaProvider.GetAgendaSala();
            if (!response.IsSuccessStatusCode)
            {
                await ErrorResponse(response, "Get");
            }

            var json = await response.Content.ReadAsStringAsync();
            JArray jsonArray = JArray.Parse(await response.Content.ReadAsStringAsync());

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<IList<AgendaSalaModel>>(jsonArray.ToString().TrimStart('{').TrimEnd('}')));
        }

        public async Task<bool> GetVerificaAgendamento(AgendaSalaModel agendaSalaModel)
        {
            var response = await _agendaSalaProvider.GetVerificaAgendamento(agendaSalaModel);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode || jsonResponse.Contains("false"))
            {
                return false;
            }
            return true;
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
