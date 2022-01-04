using NSE.Core.DomainObjects;
using System;

namespace NSE.Pedidos.Domain.Pedidos
{
    public class PedidoItem : Entity
    {
        public Guid PedidoId { get; set; }
        public Guid ProdutoId { get; set; }
        public string ProdutoName { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public string ProdutoImagem { get; set; }

        //EF Rel.
        public Pedido Pedido { get; set; }

        public PedidoItem(Guid produtoId, string produtoName, int quantidade,
            decimal valorUnitario, string produtoImagem = null)
        {
            ProdutoId = produtoId;
            ProdutoName = produtoName;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            ProdutoImagem = produtoImagem;
        }

        //EF ctor
        protected PedidoItem() { }

        internal decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }
    }
}
