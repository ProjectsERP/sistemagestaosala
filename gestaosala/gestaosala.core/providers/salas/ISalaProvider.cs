using gestaosala.core.models.sala;
using gestaosala.core.models.usuario;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace gestaosala.core.providers.salas
{
   public interface ISalaProvider
    {
        Task<HttpResponseMessage> Post(SalaModel sala);
    }
}
