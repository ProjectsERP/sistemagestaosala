using AutoMapper;
using gestaosala.core.models.usuario;
using gestaosala.ViewModels.usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestaosala.Mappers
{
#pragma warning disable CS1591
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region Usuario

            CreateMap<UsuarioModel, UsuarioViewModel>().ReverseMap();

            #endregion
        }
    }
#pragma warning restore CS1591
}

