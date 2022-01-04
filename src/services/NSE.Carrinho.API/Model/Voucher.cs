namespace NSE.Carrinho.API.Model
{
    public class Voucher
    {
        public decimal? Percentual { get; set; }
        public decimal? ValorDesconto { get; set; }
        public string Codigo { get; set; }
        public TipoDescontoValor TipoDesconto { get; set; }
    }
    public enum TipoDescontoValor
    {
        Porcentagem = 0,
        Valor = 1
    }
}
