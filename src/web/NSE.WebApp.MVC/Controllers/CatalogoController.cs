using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Services;
using System;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Controllers
{
    public class CatalogoController : MainController
    {
        private readonly ICatalogoServiceRefit _catalogoService;
        private readonly ICatalogoService _catalogoService1;
        public CatalogoController(ICatalogoServiceRefit catalogoService, ICatalogoService catalogoService1)
        {
            _catalogoService = catalogoService;
            _catalogoService1 = catalogoService1;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index()
        {
            var produtos = await _catalogoService.ObterTodos();

            return View(produtos);
        }
        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            var produtos = await _catalogoService1.ObterPorId(id);
            return View(produtos);
        }
    }
}
