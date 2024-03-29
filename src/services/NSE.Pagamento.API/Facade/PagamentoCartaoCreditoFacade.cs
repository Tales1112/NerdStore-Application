﻿using Microsoft.Extensions.Options;
using NSE.Pagamentos.API.Models;
using NSE.Pagamentos.NerdsPag;
using System;
using System.Threading.Tasks;

namespace NSE.Pagamentos.API.Facade
{
    public class PagamentoCartaoCreditoFacade : IPagamentoFacade
    {
        private readonly PagamentoConfig _pagamentConfig;
        public PagamentoCartaoCreditoFacade(IOptions<PagamentoConfig> pagamentoConfig)
        {
            _pagamentConfig = pagamentoConfig.Value;
        }
        public async Task<Transacao> AutorizarPagamento(Pagamento pagamento)
        {
            var nerdsPagSvc = new NerdsPagService(_pagamentConfig.DefaultApiKey,
                   _pagamentConfig.DefaultEncryptionKey);

            var cardHasGen = new CardHash(nerdsPagSvc)
            {
                CardNumber = pagamento.CartaoCredito.NumeroCartao,
                CardHolderName = pagamento.CartaoCredito.NomeCartao,
                CardExpirationDate = pagamento.CartaoCredito.MesAnoVencimento,
                CardCvv = pagamento.CartaoCredito.CVV
            };

            var cardHash = cardHasGen.Generate();

            var transacao = new Transaction(nerdsPagSvc)
            {
                CardHash = cardHash,
                CardNumber = pagamento.CartaoCredito.NumeroCartao,
                CardHolderName = pagamento.CartaoCredito.NomeCartao,
                CardExpirationDate = pagamento.CartaoCredito.MesAnoVencimento,
                CardCvv = pagamento.CartaoCredito.CVV,
                PaymentMethod = PaymentMethod.CreditCard,
                Amount = pagamento.Valor
            };

            return ParaTransacao(await transacao.AuthorizeCardTransaction());
        }
        public async Task<Transacao> CapturarPagamento(Transacao transacao)
        {
            var nerdsPagSvc = new NerdsPagService(_pagamentConfig.DefaultApiKey,
                _pagamentConfig.DefaultEncryptionKey);

            var transaction = ParaTransaction(transacao, nerdsPagSvc);

            return ParaTransacao(await transaction.CaptureCardTransaction());
        }

        public static Transacao ParaTransacao(Transaction transaction)
        {
            return new Transacao
            {
                Id = Guid.NewGuid(),
                Status = (StatusTransacao)transaction.Status,
                ValorTotal = transaction.Amount,
                BandeiraCartao = transaction.CardBrand,
                CodigoAutorizacao = transaction.AuthorizationCode,
                CustoTransacao = transaction.Cost,
                DataTransacao = transaction.TransactionDate,
                NSU = transaction.Nsu,
                TID = transaction.Tid
            };
        }
        public static Transaction ParaTransaction(Transacao transacao, NerdsPagService nerdsPagService)
        {
            return new Transaction(nerdsPagService)
            {
                Status = (TransactionStatus)transacao.Status,
                Amount = transacao.ValorTotal,
                CardBrand = transacao.BandeiraCartao,
                AuthorizationCode = transacao.CodigoAutorizacao,
                Cost = transacao.CustoTransacao,
                Nsu = transacao.NSU,
                Tid = transacao.TID
            };
        }

        public async Task<Transacao> CancelarAutorizacao(Transacao transacao)
        {
            var nerdsPagSvc = new NerdsPagService(_pagamentConfig.DefaultApiKey,
                 _pagamentConfig.DefaultEncryptionKey);

            var transaction = ParaTransaction(transacao, nerdsPagSvc);

            return ParaTransacao(await transaction.CancelAuthorization());
        }
    }
}
