using Desafio_Criptografia.Core.Criptografias;
using Desafio_Criptografia.Core.Enums;
using Desafio_Criptografia.Core.Services;
using Desafio_Criptografia.Core.ViewModels;

namespace Desafio_Criptografia.Core.Controllers
{
    public class CriptografiaController
    {
        private ICriptografia<string> criptografia;

        public CriptografiaController(ICriptografia<string> criptografia)
        {
            this.criptografia = criptografia;
        }

        public ObjetoCriptografia Criptografar(ObjetoCriptografia objCriptografia)
        {
            if (!ValidarObjetoCriptografiaService.ValidarOperacao(objCriptografia, EOperacao.CRIPTOGRAFAR) ||
                !ValidarObjetoCriptografiaService.ValidarTexto(objCriptografia, EOperacao.CRIPTOGRAFAR))
                return objCriptografia;

            var resultado = criptografia.Criptografar(objCriptografia.Texto);

            if (!ValidarObjetoCriptografiaService.ValidarProcessamento(objCriptografia, EOperacao.CRIPTOGRAFAR, resultado))
                return objCriptografia;

            objCriptografia.Resultado = resultado;
            objCriptografia.Hash = HashService.GetHashSha1(resultado);
            objCriptografia.statusOperacao = EStatusOperacao.PROCESSADO;

            return objCriptografia;
        }

        public ObjetoCriptografia Descriptografar(ObjetoCriptografia objCriptografia)
        {
            if (!ValidarObjetoCriptografiaService.ValidarOperacao(objCriptografia, EOperacao.DESCRIPTOGRAFAR)
                || !ValidarObjetoCriptografiaService.ValidarTexto(objCriptografia, EOperacao.DESCRIPTOGRAFAR))
                return objCriptografia;

            var resultado = criptografia.Descriptografar(objCriptografia.Texto);

            if (!ValidarObjetoCriptografiaService.ValidarProcessamento(objCriptografia, EOperacao.DESCRIPTOGRAFAR, resultado))
                return objCriptografia;

            objCriptografia.statusOperacao = EStatusOperacao.PROCESSADO;
            objCriptografia.Hash = HashService.GetHashSha1(resultado);
            objCriptografia.Resultado = resultado;

            return objCriptografia;
        }
    }
}
