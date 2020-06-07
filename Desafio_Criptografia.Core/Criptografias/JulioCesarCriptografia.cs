using Desafio_Criptografia.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desafio_Criptografia.Core.Criptografias
{
    public partial class JulioCesarCriptografia : ICriptografia<string>
    {
        private readonly int fator; //Fator de substituição
        private AlfabetoService alfabetoService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alfabetoService">Serviço para obter o alfabeto a ser utilizado</param>
        /// <param name="fator">Fator de substituição</param>
        public JulioCesarCriptografia(AlfabetoService alfabetoService, int fator)
        {
            this.fator = fator;
            this.alfabetoService = alfabetoService;
        }

        public string Criptografar(string str)
        {
            var alfabeto = alfabetoService.GetLetras();
            var tempSrt = str.ToLower();
            var letrasCorrespondentes = AlfabetoCorrespondenteEncriptar(alfabeto);

            var resultado = Substituir(alfabeto, tempSrt, letrasCorrespondentes).ToLower();

            return resultado;
        }

        public string Descriptografar(string str)
        {
            var alfabeto = alfabetoService.GetLetras();
            var tempSrt = str.ToLower();
            var letrasCorrespondentes = AlfabetoCorrespondenteDecriptar(alfabeto);

            var resultado = Substituir(alfabeto, tempSrt, letrasCorrespondentes).ToLower();

            return resultado;
        }

        private string Substituir(string[] alfabeto, string str, Dictionary<string, string> letrasCorrespondentes)
        {
            var strTemp = str.ToCharArray();

            for (int i = 0; i < strTemp.Length; i++)
            {
                if (alfabeto.Contains(strTemp[i].ToString()))
                    strTemp[i] = Convert.ToChar(letrasCorrespondentes[strTemp[i].ToString()]);
            }

            return new string(strTemp);
        }
    }
}
