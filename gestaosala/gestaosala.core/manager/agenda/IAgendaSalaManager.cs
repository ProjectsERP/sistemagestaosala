using gestaosala.core.models.agenda;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace gestaosala.core.manager.agenda
{
    public interface IAgendaSalaManager
    {
        Task<AgendaSalaModel> Insert(AgendaSalaModel agendaSala);
        Task<IList<AgendaSalaModel>> GetAgendaSala();
        Task<int> Delete(int agendamentoId, int salaId);
        Task<bool> GetVerificaAgendamento(AgendaSalaModel agendaSalaModel);
    }
}
