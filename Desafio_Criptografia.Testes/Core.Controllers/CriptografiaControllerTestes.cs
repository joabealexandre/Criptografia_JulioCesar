using Desafio_Criptografia.Core.Controllers;
using Desafio_Criptografia.Core.Criptografias;
using Desafio_Criptografia.Core.Enums;
using Desafio_Criptografia.Core.Services;
using Desafio_Criptografia.Core.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Desafio_Criptografia.Testes.Core.Controllers
{
    public class CriptografiaControllerTestes
    {
        private CriptografiaController controller;
        private Mock<ICriptografia<string>> criptografiaMock;
        private readonly string textoCriptografia = "Texto padrão para criptografia";

        public CriptografiaControllerTestes()
        {
            criptografiaMock = new Mock<ICriptografia<string>>();
        }

        [Fact]
        public void Criptografar_DeveTratarRequisicaoComOperacaoInformadaInvalida()
        {
            //Arranje
            controller = new CriptografiaController(criptografiaMock.Object);
            var obj = new ObjetoCriptografia() { Operacao = EOperacao.DESCRIPTOGRAFAR };

            //Act
            var result = controller.Criptografar(obj);

            //Assert
            Assert.Equal(EStatusOperacao.ERRO, result.statusOperacao);
        }

        [Fact]
        public void Criptografar_DeveTratarRequisicaoComTextoVazio()
        {
            //Arranje
            controller = new CriptografiaController(criptografiaMock.Object);
            var obj = new ObjetoCriptografia() { Operacao = EOperacao.CRIPTOGRAFAR };

            //Act
            var result = controller.Criptografar(obj);

            //Assert
            Assert.Equal(EStatusOperacao.ERRO, result.statusOperacao);
        }

        [Fact]
        public void Criptografar_DeveTratarRequisicaoComErroAoProcessar()
        {
            //Arranje
            criptografiaMock.Setup(c => c.Criptografar(textoCriptografia)).Returns("");
            controller = new CriptografiaController(criptografiaMock.Object);
            var obj = new ObjetoCriptografia() { Operacao = EOperacao.CRIPTOGRAFAR, Texto = textoCriptografia };

            //Act
            var result = controller.Criptografar(obj);

            //Assert
            Assert.Equal(EStatusOperacao.ERRO, result.statusOperacao);
        }

        [Fact]
        public void Descriptografar_DeveTratarRequisicaoComTextoVazio()
        {
            //Arranje
            controller = new CriptografiaController(criptografiaMock.Object);
            var obj = new ObjetoCriptografia() { Operacao = EOperacao.DESCRIPTOGRAFAR };

            //Act
            var result = controller.Descriptografar(obj);

            //Assert
            Assert.Equal(EStatusOperacao.ERRO, result.statusOperacao);
        }

        [Fact]
        public void Descriptografar_DeveTratarRequisicaoComErroAoProcessar()
        {
            //Arranje
            criptografiaMock.Setup(c => c.Criptografar(textoCriptografia)).Returns("");
            controller = new CriptografiaController(criptografiaMock.Object);
            var obj = new ObjetoCriptografia() { Operacao = EOperacao.DESCRIPTOGRAFAR, Texto = textoCriptografia };

            //Act
            var result = controller.Descriptografar(obj);

            //Assert
            Assert.Equal(EStatusOperacao.ERRO, result.statusOperacao);
        }
    }
}
