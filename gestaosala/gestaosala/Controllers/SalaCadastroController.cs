using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using gestaosala.core.manager.sala;
using gestaosala.core.models.sala;
using gestaosala.ViewModels.sala;
using Microsoft.AspNetCore.Mvc;

namespace gestaosala.Controllers
{
    public class SalaCadastroController : Controller
    {
        #region Object
        private readonly IMapper _mapper;
        private readonly ISalaManager _salaManager;

        #endregion

        #region Constructor    
        public SalaCadastroController(ISalaManager salaManager, IMapper mapper)
        {
            _salaManager = salaManager;
            _mapper = mapper;
        }
        #endregion

        [HttpGet]
        public IActionResult Post()
        {
            return View();
        }

        #region Post
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] SalaViewModel salaViewModel)
        {
            try
            {
                var sala = new SalaViewModel
                {
                    SalaTitulo = salaViewModel.SalaTitulo,
                    SalaDescricao = salaViewModel.SalaDescricao,
                   
                };

                await _salaManager.Post(_mapper.Map<SalaModel>(sala));
                return View();
            }
            catch (Exception ex)
            {
                TempData["NotifyMessage"] = "" + ex.Message;
                return BadRequest();
            }
        }
        #endregion
    }
}