﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace gestaosala.Controllers
{
    public class AgendamentoController : Controller
    {
        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}