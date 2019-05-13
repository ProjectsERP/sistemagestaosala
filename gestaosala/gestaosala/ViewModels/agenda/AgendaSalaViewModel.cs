using gestaosala.core.models.sala;
using gestaosala.ViewModels.sala;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestaosala.ViewModels.agenda
{
    public class AgendaSalaViewModel
    {
        public int SalaId { get; set; }
        public DateTime AgendamentoInicial { get; set; }
        public DateTime AgendamentoFinal { get; set; }
        public bool AgendamentoStatus { get; set; }
       // public SalaModel salaModel { get; set; }
    }
}
