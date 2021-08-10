using Entidades.Dto;
using Entidades.Modelos;
using Repositorio.Base;
using Repositorio.Contexto;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Repositorio.Repos
{
    public class HistoricoDNSRepositorio : Repositorio<HistoricoDNS>
    {
        public HistoricoDNSRepositorio(AppDbContext context) : base(context)
        {

        }

        /// <summary>
        /// Método que invoca el sp para gerar los avances de las métricas
        /// </summary>
        /// <returns></returns>
        public bool RegistrarHistorico(HistoricoDNS historicoDNS)
        {
            try
            {
                this.Adicionar(historicoDNS);
                this.context.SaveChanges();
                return true;
            }
            catch(Exception ex) { throw ex; }
        }


        /// <summary>
        /// Método que invoca el sp para gerar los avances de las métricas
        /// </summary>
        /// <returns></returns>
        public ResultadoStats ListarEstadistica()
        {
            try
            {
                decimal count_mutant_dna = dbSet.Where(t => t.mutante == true).Count();
                decimal count_human_dna = dbSet.Where(t => t.mutante == false).Count();
                decimal total = dbSet.Count();

                ResultadoStats resultadoStats = new ResultadoStats()
                {
                    count_human_dna = count_human_dna,
                    count_mutant_dna = count_mutant_dna,
                    ratio = (total == 0 ? 0 : (count_mutant_dna / total)*100)
                };
                return resultadoStats;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
