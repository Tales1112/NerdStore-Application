using NSE.Core.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NSE.WebApp.MVC.Models
{
    public class PedidoTransacaoViewModel
    {
        #region Pedido
        public decimal ValorTotal { get; set; }
        public decimal Desconto { get; set; }
        public string VoucherCodigo { get; set; }
        public bool VoucherUtilizado { get; set; }

        public List<ItemCarrinhoViewModel> Itens { get; set; } = new List<ItemCarrinhoViewModel>();
        #endregion

        #region Endereco
        public EnderecoViewModel Endereco { get; set; }
        #endregion

        #region Cartao
        [Required(ErrorMessage = "Informe o numero do cartao")]
        [DisplayName("Numero do Cartao")]
        public string NumeroCartao { get; set; }

        [Required(ErrorMessage = "Informe o nome do portador do cartao")]
        [DisplayName("Numero do Portador")]
        public string NomeCartao { get; set; }

        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "O vencimento deve estar no padrão MM/AA")]
        [CartaoExpiracao(ErrorMessage = "Cartao Expirado")]
        [Required(ErrorMessage = "Informe o vencimento")]
        [DisplayName("Data de Vencimento MM/AA")]
        public string ExpiracaoCartao { get; set; }

        [Required(ErrorMessage = "Informe o código de segurança")]
        [DisplayName("Código de Segurança")]
        public string CvvCartao { get; set; }
        #endregion
    }
}
