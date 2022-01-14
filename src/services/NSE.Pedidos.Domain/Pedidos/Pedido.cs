using NSE.Core.DomainObjects;
using NSE.Pedidos.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NSE.Pedidos.Domain.Pedidos
{
    public class Pedido : Entity, IAggregateRoot
    {
        public int Codigo { get; set; }
        public Guid ClienteId { get; set; }
        public Guid? VoucherId { get; set; }
        public bool VoucherUtilizado { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public PedidoStatus PedidosStatus { get; private set; }
        private readonly List<PedidoItem> _pedidoItems;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;
        public Endereco Endereco { get; set; }
        public Voucher Voucher { get; set; }

        public Pedido(Guid clienteId, decimal valorTotal, List<PedidoItem> pedidoItems
                     ,bool voucherUtilizado = false, decimal desconto = 0, Guid? voucherId = null)
        {
            ClienteId = clienteId;
            ValorTotal = valorTotal;
            _pedidoItems = pedidoItems;
            Desconto = desconto;
            VoucherUtilizado = voucherUtilizado;
            VoucherId = voucherId; 
        }
        public Pedido(){}

        public void AutorizarPedido()
        {
            PedidosStatus = PedidoStatus.Autorizado;
        }
        public void CancelarPedido()
        {
            PedidosStatus = PedidoStatus.Cancelado;
        }
        public void FinalizarPedido()
        {
            PedidosStatus = PedidoStatus.Pago;
        }
        public void AtribuirVoucher(Voucher voucher)
        {
            VoucherUtilizado = true;
            Voucher.Id = voucher.Id;
            Voucher = voucher;
        }
        public void AtribuirEndereco(Endereco endereco)
        {
            Endereco = endereco;
        }

        public void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(p => p.CalcularValor());
            CalculcarValorTotalDesconto();
        }
        private void CalculcarValorTotalDesconto()
        {
            if (!VoucherUtilizado) return;

            decimal desconto = 0;
            var valor = ValorTotal;

            if (Voucher.TipoDesconto == TipoDescontoVoucher.Porcentagem)
            {
                if (Voucher.Percentual.HasValue)
                {
                    desconto = (valor * Voucher.Percentual.Value) / 100;
                    valor -= desconto;
                }
            }
            else
            {
                if (Voucher.ValorDesconto.HasValue)
                {
                    desconto = Voucher.ValorDesconto.Value;
                    valor -= desconto;
                }
            }
            ValorTotal = valor < 0 ? 0 : valor;
            Desconto = desconto;
        }
    }
}
