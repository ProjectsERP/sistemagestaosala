using System;
using System.Collections.Generic;
using System.Text;

namespace gestaosala.core.models.usuario
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }
    }
}
