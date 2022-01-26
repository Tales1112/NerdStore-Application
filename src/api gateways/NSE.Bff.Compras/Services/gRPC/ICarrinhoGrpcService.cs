using NSE.Bff.Compras.Models;
using System.Threading.Tasks;

namespace NSE.Bff.Compras.Services.gRPC
{
    public interface ICarrinhoGrpcService
    {
        Task<CarrinhoDTO> ObterCarrinho();
    }
}