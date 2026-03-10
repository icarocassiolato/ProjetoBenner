using Xunit;
using Moq;
using FluentAssertions;
using System.Threading.Tasks;
using MicroondasBennerAPI.Service.Services;
using MicroondasBennerAPI.Repository.Contracts;
using MicroondasBennerAPI.Models.Base;
using MicroondasBennerAPI.Repository.Contracts;
using MicroondasBennerAPI.Models;

namespace MicroondasBennerAPI.Tests.Services;

public class ProgramasPersonalizadosServiceTests
{
    [Fact]
    public async Task InsertAsync_ShouldThrow_WhenSimboloJaUsado()
    {
        var mockRepo = new Mock<IProgramasPersonalizadosRepository>();
        // Simula que símbolo já existe no repo
        mockRepo.Setup(r => r.SimboloUtilizadoProgramaPersonalizadoAsync(It.IsAny<char>())).ReturnsAsync(true);

        var service = new ProgramasPersonalizadosService(mockRepo.Object);

        var programa = new ProgramaAquecimentoPersonalizado { Nome = "X", SimboloProgresso = 'X', Tempo = 10, Potencia = 3 };

        await Assert.ThrowsAsync<ApplicationException>(() => service.InsertAsync(programa));
    }

    [Fact]
    public async Task InsertAsync_ShouldCallRepository_WhenValid()
    {
        var mockRepo = new Mock<IProgramasPersonalizadosRepository>();
        mockRepo.Setup(r => r.SimboloUtilizadoProgramaPersonalizadoAsync(It.IsAny<char>())).ReturnsAsync(false);
        mockRepo.Setup(r => r.InsertAsync(It.IsAny<ProgramaAquecimentoPersonalizado>())).ReturnsAsync(1);

        var service = new ProgramasPersonalizadosService(mockRepo.Object);

        var programa = new ProgramaAquecimentoPersonalizado { Nome = "Valid", SimboloProgresso = 'Z', Tempo = 10, Potencia = 3 };

        var id = await service.InsertAsync(programa);
        id.Should().Be(1);
        mockRepo.Verify(r => r.InsertAsync(It.IsAny<ProgramaAquecimentoPersonalizado>()), Times.Once);
    }
}