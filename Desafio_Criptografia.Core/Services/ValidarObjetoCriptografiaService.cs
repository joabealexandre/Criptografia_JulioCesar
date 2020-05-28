using Desafio_Criptografia.Core.Enums;
using Desafio_Criptografia.Core.ViewModels;
using System;
using System.Globalization;

namespace Desafio_Criptografia.Core.Services
{
    public class ValidarObjetoCriptografiaService
    {

        public static bool ValidarOperacao(ObjetoCriptografia objCriptografia, EOperacao operacao)
        {
            if (objCriptografia.Operacao != operacao)
            {
                objCriptografia.statusOperacao = EStatusOperacao.ERRO;
                objCriptografia.Resultado = "Operação informada é inválida";
                return false;
            }
            return true;
        }

        public static bool ValidarTexto(ObjetoCriptografia objCriptografia, EOperacao operacao)
        {
            if (string.IsNullOrWhiteSpace(objCriptografia.Texto))
            {
                objCriptografia.statusOperacao = EStatusOperacao.ERRO;
                objCriptografia.Resultado = "Não foi informado o texto para a operação solicitada";
                return false;
            }
            return true;
        }

        public static bool ValidarOperacao(object objCriptografia, EOperacao cRIPTOGRAFAR)
        {
            throw new NotImplementedException();
        }

        public static bool ValidarProcessamento(ObjetoCriptografia objCriptografia, EOperacao operacao, string resultado)
        {
            if (string.IsNullOrWhiteSpace(resultado))
            {
                objCriptografia.statusOperacao = EStatusOperacao.ERRO;
                objCriptografia.Resultado =
                    $"Erro ao {CultureInfo.CurrentCulture.TextInfo.ToTitleCase(operacao.ToString().ToLower())} a mensagem";
                return false;
            }
            return true;
        }
    }
}
