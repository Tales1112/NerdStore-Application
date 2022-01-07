using NSE.Core.Communication;
using NSE.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public interface IComprasBffService
    {
        Task<ResponseResult> AdicionarItemCarrinho(ItemCarrinhoViewModel produto);
        Task<int> ObterQuantidadeCarrinho();
        Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoViewModel produto);
        Task<CarrinhoViewModel> ObterCarrinho();
        Task<ResponseResult> RemoverItemCarrinho(Guid produtoId);
        Task<ResponseResult> AplicarVoucherCarrinho(string voucher);

        //Pedido
        Task<ResponseResult> FinalizarPedido(PedidoTransacaoViewModel pedidoTransacao);
        Task<PedidoViewModel> ObterUltimoPedido();
        Task<IEnumerable<PedidoViewModel>> ObterListaPorClienteId();
        PedidoTransacaoViewModel MapearParaPedido(CarrinhoViewModel carrinho, EnderecoViewModel endereco);

    }
}