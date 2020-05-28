using Desafio_Criptografia.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Desafio_Criptografia.Testes.Core.Services
{
    public class AlfabetoServiceTestes
    {
        private string[] letras = null;
        private AlfabetoService service;

        public AlfabetoServiceTestes()
        {
            service = new AlfabetoService();
            letras = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        }

        [Fact]
        public void GetLetras_DeveRetonarAlfabetoCompleto()
        {
            //Act
            var result = service.GetLetras();
            var resultHash = new HashSet<string>(result); //Removendo duplicidades
            var resultIntegridade = CompararIntegridadeAlfabetos(resultHash); 

            //Assert
            Assert.Equal(letras.Length, result.Length);
            Assert.Equal(letras.Length, resultHash.Count);
            Assert.Equal(letras.Length, resultIntegridade.Count);
        }

        [Fact]
        public void GetLetras_DeveRetornarAlfabetoMaisculo()
        {
            //Act
            var result = service.GetLetras(true);
            var resultIntegridade = CompararIntegridadeAlfabetos(result, true);

            //Assert
            Assert.Equal(resultIntegridade.Count, result.Length);
        }

        /// <summary>
        /// Compara a igualdade de duas lista contendo o alfabeto
        /// </summary>
        /// <param name="lista">Lista com o alfabeto a ser comparado</param>
        /// <param name="letraMaiuscula">Indica se a lista base da classe de teste deve conter letras maiúsculas ou minúsculas</param>
        /// <returns></returns>
        private List<string> CompararIntegridadeAlfabetos(ICollection<string> lista, bool letraMaiuscula = false)
        {
            if (!letraMaiuscula)
                return letras.Where(l => lista.Contains(l)).ToList();
            else
                return letras.Where(l => lista.Contains(l.ToUpper())).ToList();
        }

    }
}
