using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Desafio_Criptografia.Core.Services
{
    public class AlfabetoService
    {
        private string[] letras = null;

        public AlfabetoService()
        {
            letras = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        }


        /// <summary>
        /// Retorna um array com todas as letras do alfabeto.
        /// </summary>
        /// <returns></returns>
        public string[] GetLetras(bool letrasMaiusculas = false)
        {
            if (!letrasMaiusculas)
                return letras;
            else
            {
                return letras.Select(l => l.ToUpper()).ToArray();
            }
        }
    }
}
