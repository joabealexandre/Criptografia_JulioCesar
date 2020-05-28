using Desafio_Criptografia.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Desafio_Criptografia.Testes.Core.Services
{
    public class HashServiceTestes
    {
        [Fact]
        public void GetHashSha1_DeveRetornarHashCorreto()
        {
            //Arrange
            var texto = "Texto para teste";
            var expected = "37ffcc005f7bba7ff03ad429fcffda3e40d5fe83";

            //Act
            var result = HashService.GetHashSha1(texto);
           
            //Assert
            Assert.Equal(expected, result);
        }
    }
}
