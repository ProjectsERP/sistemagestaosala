using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using gestaosala.core.manager.usuario;
using gestaosala.core.models.usuario;
using gestaosala.Util;
using gestaosala.ViewModels.login;
using gestaosala.ViewModels.usuario;
using Microsoft.AspNetCore.Mvc;

namespace gestaosala.Controllers
{

    public class UsuarioController : Controller
    {
        #region Object
        private readonly IMapper _mapper;
        private readonly IUsuarioManager _usuarioManager;

        #endregion

        #region Constructor    
        public UsuarioController(IUsuarioManager usuarioManager, IMapper mapper)
        {
            _usuarioManager = usuarioManager;
            _mapper = mapper;
        }
        #endregion

        #region Get

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        #endregion

        #region Post
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] UsuarioViewModel usuario)
        {
            try
            {
                var user = new UsuarioViewModel
                {
                    Nome = usuario.Nome,
                    Login = usuario.Login,
                    Senha = usuario.Senha
                };

                await _usuarioManager.Insert(_mapper.Map<UsuarioModel>(user));
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["NotifyMessage"] = "" + ex.Message;
                return BadRequest();
            }
        }
        #endregion

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        #region Post
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new LoginViewModel
                    {
                        Login = usuario.Login,
                        Senha = Hash.GerarHash(usuario.Senha)
                    };

                    bool usuarioLogin = await _usuarioManager.GetLogin(_mapper.Map<UsuarioModel>(user));
                    if (usuarioLogin)
                    {
                        ViewBag.message = "Usuario " + user.Login + " logado";
                        return RedirectToAction("Post", "SalaCadastro");
                    }
                }
                ViewBag.message = "Usuario ou senha incorreto";
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