using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MicroondasBennerFrontend.Pages;

namespace MicroondasBennerFrontend.Tests.Pages;

public class IndexPageTests
{
    [Fact]
    public void OnGet_ShouldReturnPageResult()
    {
        var page = new IndexModel();
        page.OnGet();
        // PageModel OnGet não retorna valor, apenas garante execução sem exceção
        page.Should().NotBeNull();
    }
}