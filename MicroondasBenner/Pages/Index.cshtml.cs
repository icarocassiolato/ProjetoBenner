using System.ComponentModel;
using MicroondasBenner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroondasBenner.Pages;

public class IndexModel : PageModel
{
    private readonly MicroondasService _microondasService = new();

    [BindProperty] public int Tempo { get; set; }
    [BindProperty] public int Potencia { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Mensagem { get; set; } = string.Empty;

    public void OnGet()
    {

    }

    public IActionResult OnPostIniciar()
    {
        _microondasService.Iniciar(Tempo, Potencia);

        var erro = _microondasService.Validar();
        if (!string.IsNullOrEmpty(erro))
        {
            Mensagem = erro;
            return Page();
        }

        Status = _microondasService.Model.BarraProcesso;
        return Page();
    }

    public IActionResult OnPostPausar()
    {
        _microondasService.Pausar();
        Status = _microondasService.Model.BarraProcesso;
        return Page();
    }

    public IActionResult OnPostCancelar()
    {
        _microondasService.Cancelar();
        Status = _microondasService.Model.BarraProcesso;
        return Page();
    }

    public IActionResult OnPostInicioRapido()
    {
        _microondasService.InicioRapido();
        Status = _microondasService.Model.BarraProcesso;
        return Page();
    }
}
