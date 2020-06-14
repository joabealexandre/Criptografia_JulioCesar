using Desafio_Criptografia.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio_Criptografia.Core.ViewModels
{
    public class ObjetoCriptografia
    {
        public int Fator { get; set; }

        public string Texto { get; set; }

        public string Resultado { get; set; }

        public string Hash { get; set; }

        public EOperacao Operacao { get; set; }

        public EStatusOperacao statusOperacao { get; set; }
    }
}
