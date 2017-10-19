using System;
using Infrastructure.TorreHanoi.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.TorreHanoi.Domain
{
    [TestClass]
    public class TorreHanoiUnit
    {
        private const string CategoriaTeste = "Domain/TorreHanoi";

        private Mock<ILogger> _mockLogger;

        [TestInitialize]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger>();
            _mockLogger.Setup(s => s.Logar(It.IsAny<string>(), It.IsAny<TipoLog>()));
        }
        
        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Construtor_Deve_Retornar_Sucesso()
        {
            // Esse teste já tinha sido implementado em Tests.TorreHanoi.Domain.PinoUnit.Construtor_Deve_Retornar_Sucesso()
            // Talvez seja interessante testar o construtor de Domain.TorreHanoi.Pino individualmente.

            var torre = new global::Domain.TorreHanoi.TorreHanoi(3, _mockLogger.Object);

            Assert.IsNotNull(torre);
            Assert.IsNotNull(torre.Destino);
            Assert.IsNotNull(torre.Origem);
            Assert.IsNotNull(torre.Intermediario);
            Assert.AreEqual(torre.Origem.Tipo, global::Domain.TorreHanoi.TipoPino.Origem);
            Assert.AreEqual(torre.Destino.Tipo, global::Domain.TorreHanoi.TipoPino.Destino);
            Assert.AreEqual(torre.Intermediario.Tipo, global::Domain.TorreHanoi.TipoPino.Intermediario);
            Assert.AreEqual(torre.Intermediario.Discos.Count, 0);
            Assert.AreEqual(torre.Destino.Discos.Count, 0);
            Assert.AreEqual(torre.Origem.Discos.Count, 3);

            Assert.AreNotEqual(torre.Id, new Guid());
            Assert.AreNotEqual(torre.DataCriacao, new DateTime());
            Assert.AreEqual(torre.Status, global::Domain.TorreHanoi.TipoStatus.Pendente);
            Assert.IsNotNull(torre.PassoAPasso);
        }

        [TestMethod]
        [TestCategory(CategoriaTeste)]
        public void Processar_Deverar_Retornar_Sucesso()
        {
            var torre = new global::Domain.TorreHanoi.TorreHanoi(3, _mockLogger.Object);

            // Validando estado inicial.
            Assert.AreEqual(torre.Origem.Discos.Count, 3, "Estado inicial inesperado. Pino Origem não contem o numero correto de discos.");
            Assert.AreEqual(torre.Intermediario.Discos.Count, 0, "Estado inicial inesperado. Pino Intermediario contem discos.");
            Assert.AreEqual(torre.Destino.Discos.Count, 0, "Estado inicial inesperado. Pino Destino contem discos.");

            torre.Processar();

            // Validando estado final.
            Assert.AreEqual(torre.Status, global::Domain.TorreHanoi.TipoStatus.FinalizadoSucesso);
            Assert.AreEqual(torre.Origem.Discos.Count, 0, "Estado final inesperado. Pino Origem contem discos.");
            Assert.AreEqual(torre.Intermediario.Discos.Count, 0, "Estado final inesperado. Pino Intermediario contem discos.");
            Assert.AreEqual(torre.Destino.Discos.Count, 3, "Estado final inesperado. Pino Destino não contem o numero correto de discos.");
            Assert.AreNotEqual(torre.PassoAPasso.Count, 0, "Passo a passo não foi armazenado.");
            
        }
    }
}
