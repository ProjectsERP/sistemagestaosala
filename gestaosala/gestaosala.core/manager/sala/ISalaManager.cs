using gestaosala.core.models.sala;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace gestaosala.core.manager.sala
{
    public interface ISalaManager
    {
        Task<SalaModel> Post(SalaModel sala);
    }
}
