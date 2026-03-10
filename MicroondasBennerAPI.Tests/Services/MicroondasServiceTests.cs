using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.SignalR;
using MicroondasBennerAPI.Service.Services;
using MicroondasBennerAPI.Hubs;
using MicroondasBennerAPI.Models;
using MicroondasBennerAPI.Models.Base;
using MicroondasBennerAPI.Models.Enums;
using MicroondasBennerAPI.Helpers;
using FluentAssertions;
using System;
using MicroondasBennerAPI.Models.Enums;

namespace MicroondasBennerAPI.Tests.Services;

public class MicroondasServiceTests
{
    [Fact]
    public void Iniciar_ShouldStartProgramAndUpdateStatus()
    {
        // Arrange - mock IHubContext
        var mockClientProxy = new Mock<IClientProxy>();
        mockClientProxy
            .Setup(p => p.SendCoreAsync(It.IsAny<string>(), It.IsAny<object[]>(), default))
            .Returns(Task.CompletedTask);

        var mockClients = new Mock<IHubClients>();
        mockClients.Setup(c => c.All).Returns(mockClientProxy.Object);

        var mockHubContext = new Mock<IHubContext<MicroondasHub>>();
        mockHubContext.Setup(h => h.Clients).Returns(mockClients.Object);

        var service = new MicroondasService(mockHubContext.Object);

        // Use program default (Padrao) via GetConfigPrograma
        var programa = service.GetConfigPrograma(ETipoPrograma.Padrao);

        // Act
        service.Iniciar(programa);

        // Assert initial state
        service.EmAndamento.Should().BeTrue();
        service.Pausado.Should().BeFalse();
        var status = service.GetStatusAtual();
        status.Should().Contain("restantes"); // formato esperado

        // Simulate one tick
        service.Timer();

        // After one Timer call, tempo decreased and StringProgresso has appended symbols
        service.GetStatusAtual().Should().Contain(programa.SimboloProgresso.ToString());
        mockClientProxy.Verify(p => p.SendCoreAsync(
            It.Is<string>(s => s == MetodoHelper.AtualizarProgresso),
            It.IsAny<object[]>(),
            default), Times.AtLeastOnce);
    }

    [Fact]
    public void Adicionar30Segundos_ShouldIncreaseTime_WhenValid()
    {
        var mockClientProxy = new Mock<IClientProxy>();
        mockClientProxy.Setup(p => p.SendCoreAsync(It.IsAny<string>(), It.IsAny<object[]>(), default)).Returns(Task.CompletedTask);
        var mockClients = new Mock<IHubClients>(); mockClients.Setup(c => c.All).Returns(mockClientProxy.Object);
        var mockHubContext = new Mock<IHubContext<MicroondasHub>>(); mockHubContext.Setup(h => h.Clients).Returns(mockClients.Object);

        var service = new MicroondasService(mockHubContext.Object);
        var programa = service.GetConfigPrograma(ETipoPrograma.Padrao);
        programa.Tempo = 30;
        programa.Potencia = 5;

        service.Iniciar(programa);
        var before = service.GetStatusAtual();

        service.Adicionar30Segundos();

        service.GetStatusAtual().Should().NotBe(before);
    }
}