using gestaosala.core.models.agenda;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace gestaosala.core.providers.agenda
{
    public interface IAgendaSalaProvider
    {
        Task<HttpResponseMessage> Post(AgendaSalaModel agendaSala);
        Task<HttpResponseMessage> GetAgendaSala();
        Task<HttpResponseMessage> Delete(int agendamentoId, int salaId);
        Task<HttpResponseMessage> GetVerificaAgendamento(AgendaSalaModel agendaSalaModel);
    }
}
