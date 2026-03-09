using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroondasBenner.Pages;

public class LoginModel : PageModel
{
    //Evitei usar lógica de negócios aqui, essa classe deve ser apenas para lidar com requisições HTTP e passar dados para a view.
    //A lógica de controle do microondas está toda na MicroondasService e a comunicação em tempo real é feita via SignalR,
    //então não tem nada a ser feito aqui por enquanto.
    public void OnGet()
    {
    }
}