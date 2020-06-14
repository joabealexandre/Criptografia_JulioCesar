using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desafio_Criptografia.Core.Criptografias
{
    public partial class JulioCesarCriptografia
    {
        private Dictionary<string, string> AlfabetoCorrespondenteEncriptar(string[] alfabeto)
        {
            var dicionario = new Dictionary<string, string>();
            var fatortemp = fator;
            var indexTemp = 0;

            for (int i = 0; i < alfabeto.Length; i++)
            {
                if (i == alfabeto.Length - fatortemp)
                {
                    dicionario.Add(alfabeto[i], alfabeto[indexTemp++]);
                    fatortemp--;
                }
                else
                    dicionario.Add(alfabeto[i], alfabeto[i + fator]);
            }
            return dicionario;
        }

        private Dictionary<string, string> AlfabetoCorrespondenteDecriptar(string[] alfabeto)
        {
            var dicionario = new Dictionary<string, string>();
            var fatortemp = fator - 1;
            var indexTemp = 1;

            for (int i = alfabeto.Length - 1; i >= 0; i--)
            {
                if (i == fatortemp)
                {
                    dicionario.Add(alfabeto[i], alfabeto[alfabeto.Length - indexTemp++]);
                    fatortemp--;
                }
                else
                    dicionario.Add(alfabeto[i], alfabeto[i - fator]);
            }
            return dicionario;
        }
    }
}
