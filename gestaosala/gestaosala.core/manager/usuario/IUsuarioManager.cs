using gestaosala.core.models.usuario;
using gestaosala.core.providers.usuario;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace gestaosala.core.manager.usuario
{
    public interface IUsuarioManager
    {
       Task<UsuarioModel> Insert(UsuarioModel usuario);
        Task<bool> GetLogin(UsuarioModel usuario);
    }
}
