using Entidades.Dto;
using Entidades.Modelos;
using Newtonsoft.Json;
using Repositorio.Contexto;
using Repositorio.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocio
{

    /// <summary>
    /// Clase MutantNegocio
    /// </summary>
    public class MutantNegocio
    {
        private int n { get; set; }
        private int cantidadLetras;
        private string[,] dna { get; set; }
        private List<string> secuencia { get; set; }
        private string[] vocabulario { get; set; }
        private AppDbContext context;


        public MutantNegocio(AppDbContext context)
        {
            this.vocabulario = new string[] { "A", "T", "C", "G" };
            this.cantidadLetras = 4;
            this.secuencia = new List<string>();
            this.ContruirSecuencia();
            this.context = context;
        }


        /// <summary>
        /// Metodo que construye la secuencia a buscar
        /// </summary>
        /// <returns></returns>
        private void ContruirSecuencia()
        {
            string[] secuencia = new string[this.vocabulario.Length];

            string letras = string.Empty;
            foreach (var item in vocabulario)
            {
                letras = string.Empty;
                for (int i = 0; i < cantidadLetras; i++)
                {
                    letras += item;
                }

                this.secuencia.Add(letras);
            }
        }

        /// <summary>
        /// Método que consulta la estadisticas de los dna
        /// </summary>
        /// <returns></returns>
        public ResultadoStats Stats()
        {
            //Registrar el resultado
            HistoricoDNSRepositorio historicoDNSRepositorio = new HistoricoDNSRepositorio(this.context);
            return historicoDNSRepositorio.ListarEstadistica();
        }




        /// <summary>
        /// Método que permite validar si la secuencia dada corresponde a un mutante
        /// </summary>
        /// <returns></returns>
        public bool isMutant(SolictudValidacionDTO solictudValidacionDTO)
        {
            InicializarMatriz(solictudValidacionDTO);
            bool isMutante = false;
            int ocurrencias = VerificarDiagonalSuperiorInferior() + VerificarDiagonalInferiorSuperior() + VerificarVertical() + VerificarHorizontal();
            if (ocurrencias > 1)
                isMutante = true;

            //Registrar el resultado
            HistoricoDNS historicoDNS = new HistoricoDNS() { dns = JsonConvert.SerializeObject(solictudValidacionDTO), mutante = isMutante };
            HistoricoDNSRepositorio historicoDNSRepositorio = new HistoricoDNSRepositorio(this.context);
            historicoDNSRepositorio.RegistrarHistorico(historicoDNS);
            return isMutante;
        }


        /// <summary>
        /// Metodo que permite inicializar la matriz con la que se trabajará
        /// </summary>
        /// <param name="solictudValidacionDTO"></param>
        private void InicializarMatriz(SolictudValidacionDTO solictudValidacionDTO)
        {
            string secuencia = string.Empty;
            int n = solictudValidacionDTO.dna.First().Length;
            string[,] letras = new string[n, n];

            foreach (var item in solictudValidacionDTO.dna)
                secuencia += item;

            //Pasamos la información  a una matríx para mejor manejo
            int i = 0; int j = 0; int count = 0;
            for (int l = 0; l < secuencia.Length; l++)
            {
                count++;
                letras[j, i] = secuencia.ElementAt(l).ToString();
                i++;
                if (count == n)
                {
                    i = 0; j++; count = 0;
                }
            }
           
            this.dna = letras;
            this.n = n;
        }


        /// <summary>
        /// Consulta las ocurrencias en la diagonal inferior a superior
        /// </summary>
        /// <returns></returns>
        private int VerificarDiagonalInferiorSuperior()
        {
            int ocurrencias = 0;
            string cadena = string.Empty;
            for (int i = 0; i < n * 2; i++)
            {
                cadena = string.Empty;
                for (int j = 0; j <= i; j++)
                {
                    int k = i - j;
                    if (k < n && j < n)
                    {
                        cadena += this.dna[j, k];
                    }
                }
                ocurrencias += ContarOccurencias(cadena);
            }

            return ocurrencias;
        }



        /// <summary>
        /// Consulta las ocurrencias  en la diagonal  superior a inferior
        /// </summary>
        /// <returns></returns>
        private int VerificarDiagonalSuperiorInferior()
        {
            int ocurrencias = 0;
            string cadena = string.Empty;
            for (int i = 1 - n; i <= n - 1; i++)
            {
                cadena = string.Empty;
                for (int j = Math.Max(0, i), k = -Math.Min(0, i); j < n && k < n; j++, k++)
                {
                    cadena += this.dna[j, k];
                }

                ocurrencias += ContarOccurencias(cadena);
            }

            return ocurrencias;
        }

        /// <summary>
        /// Consulta las ocurrencias verticalmente
        /// </summary>
        /// <returns></returns>
        private int VerificarVertical()
        {
            int ocurrencias = 0;
            string cadena = string.Empty;

            for (var i = 0; i < n; i++)
            {
                cadena = string.Empty;
                for (var j = 0; j < n; j++)
                {
                    cadena += this.dna[j, i];
                }

                ocurrencias += ContarOccurencias(cadena);
            }

            return ocurrencias;
        }

        /// <summary>
        /// Consulta las ocurrencias horizontalmente
        /// </summary>
        /// <returns></returns>
        private int VerificarHorizontal()
        {
            int ocurrencias = 0;
            string cadena = string.Empty;

            for (var i = 0; i < n; i++)
            {
                cadena = string.Empty;
                for (var j = 0; j < n; j++)
                {
                    cadena += this.dna[i, j];
                }

                ocurrencias += ContarOccurencias(cadena);
            }
            return ocurrencias;
        }

        /// <summary>
        /// Cuenta las ocuerrencias 
        /// </summary>
        /// <param name="baseSecuencia"></param>
        /// <returns></returns>
        private int ContarOccurencias(string baseSecuencia)
        {
            int ocurrencias = 0;
            foreach (var item in this.secuencia)
            {
                if (baseSecuencia.Contains(item))
                    ocurrencias++;
            }

            return ocurrencias;
        }
    }
}
