using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using gestaosala.core.manager.agenda;
using gestaosala.core.manager.sala;
using gestaosala.core.models.agenda;
using gestaosala.core.models.sala;
using gestaosala.ViewModels.agenda;
using gestaosala.ViewModels.sala;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace gestaosala.Controllers
{
    public class AgendamentoController : Controller
    {
        #region Object
        private readonly IMapper _mapper;
        private readonly IAgendaSalaManager _agendaSalaManager;
        private readonly ISalaManager _salaManager;
       
        #endregion

        #region Constructor    
        public AgendamentoController(IAgendaSalaManager agendaSalaManager,
                                     ISalaManager salaManager, IMapper mapper)
        {
            _agendaSalaManager = agendaSalaManager;
            _salaManager = salaManager;
            _mapper = mapper;
        }
        #endregion

        [HttpGet]
        public IActionResult Index()
        {
          
            return View();
        }

        public async Task<IActionResult> _GetAgendamentoSalas()
        {
            try
            {

                IList<SalaModel> agendas = new List<SalaModel>();
                var agendamentoSala = await _salaManager.GetSalas();
                foreach(var i in agendamentoSala)
                {
                    SalaModel agenda = new SalaModel();
                    agenda.SalaId = i.SalaId;
                    agenda.SalaTitulo = i.SalaTitulo;
                    agenda.SalaDescricao = i.SalaDescricao;
                    agendas.Add(agenda);
                }

                TempData["AgendamentoSalas"] = agendas;
                var agendamentos = JsonConvert.SerializeObject(agendas);
                return PartialView(agendas);
            }
            catch (Exception ex)
            {
                TempData["NotifyMessage"] = "" + ex.Message;
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Post()
        {
            try
            {
  
                //IList<SalaCadastroViewModel> salas = new List<SalaCadastroViewModel>();

                //salas = _mapper.Map<IList<SalaCadastroViewModel>>( await _salaManager.GetSalas());

                ViewBag.Combo =  _mapper.Map<IList<SalaCadastroViewModel>>(await _salaManager.GetSalas());

                return View();
            }
            catch (Exception ex)
            {
                TempData["NotifyMessage"] = "" + ex.Message;
                return BadRequest();
            }          
        }

        #region Post
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AgendaSalaViewModel agendaSalaViewModel)
        {
            try
            {
                await _agendaSalaManager.Insert(_mapper.Map<AgendaSalaModel>(agendaSalaViewModel));
                return RedirectToAction("Index", "Agendamento");
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