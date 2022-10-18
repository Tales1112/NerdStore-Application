using Microsoft.AspNetCore.Mvc;
using NSE.Core.Communication;
using System.Linq;

namespace NSE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponsePossuiErros(ResponseResult resposta)
        {
            if(resposta != null && resposta.Errors.Mensagens.Any())
            {
                foreach (var mensagem in resposta.Errors.Mensagens)
                {
                    ModelState.AddModelError(string.Empty, mensagem);
                }
                return true;
            }
            return false;
        }
        protected void AdicionarErroValidacao(string message)
        {
            ModelState.AddModelError(string.Empty, message);
        }

        protected bool OperacaoValida()
        {
            return ModelState.ErrorCount == 0;
        }
    }
}
