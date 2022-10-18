using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public class CatalogoService : Service, ICatalogoService
    {
        private readonly HttpClient _httpCient;
        public CatalogoService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
           
            httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);
            _httpCient = httpClient;
        }
        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            var response = await _httpCient.GetAsync($"/catalogo/produtos/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<ProdutoViewModel>(response);
        }
        public async Task<PagedViewModel<ProdutoViewModel>> ObterTodos(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpCient.GetAsync($"/catalogo/produtos?ps={pageSize}&page={pageIndex}&q={query}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedViewModel<ProdutoViewModel>>(response);
        }
    }
}
