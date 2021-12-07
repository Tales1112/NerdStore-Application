using Microsoft.AspNetCore.Authorization;
using NSE.WEbApi.Core.Controllers;

namespace NSE.Carrinho.API.Controllers
{
    [Authorize]
    public class CarrinhoController : MainController
    {
    }
}
