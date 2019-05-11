using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gestaosala.ViewModels.sala
{
    public class SalaViewModel
    {
        [Required(ErrorMessage = "Título Obrigatório")]
        public string SalaTitulo { get; set; }
        [Required(ErrorMessage = "Descrição Obrigatório")]
        public string SalaDescricao { get; set; }
    }
}
