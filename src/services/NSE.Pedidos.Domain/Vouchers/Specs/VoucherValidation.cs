using NetDevPack.Specification;

namespace NSE.Pedidos.Domain.Vouchers.Specs
{
    public class VoucherValidation : SpecValidator<Voucher>
    {
        public VoucherValidation()
        {
            var dataSpec = new VoucherDataSpecification();
            var qtedSpec = new VoucherQuantidadeSpecification();
            var ativoSpec = new VoucherAtivoSpecification();

            Add("dataSpec", new Rule<Voucher>(dataSpec, "Este voucher esta expirado"));
            Add("qtedSpec", new Rule<Voucher>(qtedSpec, "Este voucher ja foi utilizado"));
            Add("dataSpec", new Rule<Voucher>(ativoSpec, "Este voucher nao esta mais ativo"));
        }
    }
}
