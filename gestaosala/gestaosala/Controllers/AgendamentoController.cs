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
                IList<AgendaSalaViewModel> agendas = new List<AgendaSalaViewModel>();
                agendas = _mapper.Map<List<AgendaSalaViewModel>>(await _agendaSalaManager.GetAgendaSala());

                foreach (var agenda in agendas)
                {
                    agenda.salaModel = (await _salaManager.GetSalasBySalaId(agenda.SalaId));
                }

                TempData["AgendamentoSalas"] = agendas;
                //var agendamentos = JsonConvert.SerializeObject(agendas);
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
                ViewBag.Combo = _mapper.Map<IList<SalaCadastroViewModel>>(await _salaManager.GetSalas());

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
                var verificaSalaAgendada = _agendaSalaManager.GetVerificaAgendamento(_mapper.Map<AgendaSalaModel>(agendaSalaViewModel));

                if(agendaSalaViewModel.AgendamentoFinal < agendaSalaViewModel.AgendamentoInicial)
                {
                    ViewBag.message = "Data Inicial maior que data final";
                    ViewBag.Combo = _mapper.Map<IList<SalaCadastroViewModel>>(await _salaManager.GetSalas());
                    return View();
                }

                if( verificaSalaAgendada.Result == true)
                {
                    ViewBag.message = "Sala ja agendada nesse periodo";
                    ViewBag.Combo = _mapper.Map<IList<SalaCadastroViewModel>>(await _salaManager.GetSalas());
                    return View();
                }
                else
                {
                    await _agendaSalaManager.Insert(_mapper.Map<AgendaSalaModel>(agendaSalaViewModel));
                    return RedirectToAction("Index", "Agendamento");
                }
            }
    
            catch (Exception ex)
            {
                TempData["NotifyMessage"] = "" + ex.Message;
                return BadRequest();
            }
        }
        #endregion

        public async Task<int> Deletar(int agendamentoId, int salaId)
        {
            try
            {
                var sala = await _agendaSalaManager.Delete(agendamentoId, salaId);
                return sala;
            }
            catch (Exception ex)
            {
                TempData["NotifyMessage"] = "" + ex.Message;
                return -1;
            }
        }
    }
}