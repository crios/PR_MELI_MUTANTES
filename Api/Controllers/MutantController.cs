using Entidades.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using Repositorio.Contexto;
using System;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MutantController : ControllerBase
    {

        #region Atributos
        private readonly MutantNegocio negocio;
        #endregion

        public MutantController(AppDbContext context) => negocio ??= new MutantNegocio(context);

        /// <summary>
        /// Servicio que recibe un listado de dna y valida si el humano es un mutanto o no
        /// </summary>
        /// <param name="dna"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(SolictudValidacionDTO solictudValidacionDTO)
        {
            try
            {
                bool isMutant = negocio.isMutant(solictudValidacionDTO);
                if (isMutant)
                    return StatusCode(StatusCodes.Status200OK, isMutant);
                else
                    return StatusCode(StatusCodes.Status403Forbidden, isMutant);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       /// <summary>
       /// Método que consulta la estadisticas de los dna
       /// </summary>
       /// <returns></returns>
        [HttpGet]
        [Route("stats")]
        public IActionResult stats()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, negocio.Stats());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}