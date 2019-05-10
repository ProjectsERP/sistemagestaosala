using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gestaosala.ViewModels.usuario
{
    public class UsuarioViewModel
    {

        [Required(ErrorMessage = "Nome Obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Login Obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Senha Obrigatório")]
        public string Senha { get; set; }

    }
}
