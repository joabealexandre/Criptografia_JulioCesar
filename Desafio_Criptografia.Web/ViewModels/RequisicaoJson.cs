using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio_Criptografia.Web.Core.ViewModels
{
    public class RequisicaoJson
    {
        public int numero_casas { get; set; }
        public string token { get; set; }
        public string cifrado { get; set; }
        public string decifrado { get; set; }
        public string resumo_criptografico { get; set; }
    }
}
