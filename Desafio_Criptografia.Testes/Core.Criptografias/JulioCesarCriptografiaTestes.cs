using Desafio_Criptografia.Core.Criptografias;
using Desafio_Criptografia.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Desafio_Criptografia.Testes.Core.Criptografias
{
    public class JulioCesarCriptografiaTestes
    {
        private JulioCesarCriptografia criptografia;

        [Fact]
        public void Criptografar_DeveCriptografarTextoApenasLetrasFator3()
        {
            //Arranje
            criptografia = new JulioCesarCriptografia(new AlfabetoService(), 3);
            var texto = "a ligeira raposa marrom saltou sobre o cachorro cansado";
            var expected = "d oljhlud udsrvd pduurp vdowrx vreuh r fdfkruur fdqvdgr";

            //Act
            var result = criptografia.Criptografar(texto);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Criptografar_DeveCriptografarTextoApenasLetrasFator4()
        {
            //Arranje
            criptografia = new JulioCesarCriptografia(new AlfabetoService(), 4);
            var texto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var expected = "EFGHIJKLMNOPQRSTUVWXYZABCD".ToLower();

            //Act
            var result = criptografia.Criptografar(texto);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Criptografar_DeveCriptografarTextoLetrasEPontosFator3()
        {
            //Arranje
            criptografia = new JulioCesarCriptografia(new AlfabetoService(), 3);
            var texto = "1a.a";
            var expected = "1d.d";

            //Act
            var result = criptografia.Criptografar(texto);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Descriptografar_DeveDescriptografarTextoApenasLetrasFator3()
        {
            //Arranje
            criptografia = new JulioCesarCriptografia(new AlfabetoService(), 3);
            var texto = "d oljhlud udsrvd pduurp vdowrx vreuh r fdfkruur fdqvdgr";
            var expected = "a ligeira raposa marrom saltou sobre o cachorro cansado";

            //Act
            var result = criptografia.Descriptografar(texto);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Descriptografar_DeveDescriptografarTextoLetrasEPontosFator3()
        {
            //Arranje
            criptografia = new JulioCesarCriptografia(new AlfabetoService(), 3);
            var texto = "1d.d";
            var expected = "1a.a";

            //Act
            var result = criptografia.Descriptografar(texto);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
