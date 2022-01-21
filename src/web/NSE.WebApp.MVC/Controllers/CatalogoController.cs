using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Services;
using System;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Controllers
{
    public class CatalogoController : MainController
    {
        private readonly ICatalogoServiceRefit _catalogoService1;
        private readonly ICatalogoService _catalogoService;
        public CatalogoController(ICatalogoServiceRefit catalogoService, ICatalogoService catalogoService1)
        {
            _catalogoService1 = catalogoService;
            _catalogoService = catalogoService1;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var produtos = await _catalogoService.ObterTodos(ps, page, q);
            ViewBag.Pesquisa = q;
            produtos.ReferenceAction = "Index";

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
