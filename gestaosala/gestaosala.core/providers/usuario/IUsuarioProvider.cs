using gestaosala.core.models.usuario;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace gestaosala.core.providers.usuario
{
    public interface IUsuarioProvider
    {
        Task<HttpResponseMessage> Insert(UsuarioModel usuario);
        Task<HttpResponseMessage> GetLogin(UsuarioModel usuario);
    }
}
