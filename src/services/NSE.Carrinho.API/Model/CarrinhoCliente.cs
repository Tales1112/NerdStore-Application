﻿using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NSE.Carrinho.API.Model
{
    public class CarrinhoCliente
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
        public List<CarrinhoItem> Itens { get; set; } = new List<CarrinhoItem>();
        public ValidationResult ValidationResult { get; set; }
        public bool VoucherUtilizado { get; set; }  
        public decimal Desconto { get; set; }
        public Voucher Voucher { get; set; }
        public CarrinhoCliente(Guid clienteId)
        {
            Id = Guid.NewGuid();
            ClienteId = clienteId;
        }

        public CarrinhoCliente()
        {

        }

        public void AplicarVoucher(Voucher voucher)
        {
            Voucher = voucher;
            VoucherUtilizado = true;
            CalcularValorCarrinho();
        }
        internal void CalcularValorCarrinho()
        {
            ValorTotal = Itens.Sum(p => p.CalcularValor());
            CalculcarValorTotalDesconto();
        }
        private void CalculcarValorTotalDesconto()
        {
            if (!VoucherUtilizado) return;

            decimal desconto = 0;
            var valor = ValorTotal;

            if(Voucher.TipoDesconto == TipoDescontoValor.Porcentagem)
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
        internal bool CarrinhoItemExistente(CarrinhoItem item)
        {
            return Itens.Any(p => p.ProductId == item.ProductId);
        }
        internal CarrinhoItem ObterPorProdutoId(Guid produtId)
        {
            return Itens.FirstOrDefault(p => p.ProductId == produtId);
        }
        internal void AtualizarItem(CarrinhoItem item)
        {
            item.AssociarCarrinho(Id);

            var itemExistente = ObterPorProdutoId(item.ProductId);

            Itens.Remove(itemExistente);
            Itens.Add(item);

            CalcularValorCarrinho();
        }
        internal void AtualizarUnidades(CarrinhoItem item, int unidades)
        {
            item.AtualizarUnidades(unidades);
            AtualizarItem(item);
        }
        internal void RemoverItem(CarrinhoItem item)
        {
            Itens.Remove(ObterPorProdutoId(item.ProductId));
            CalcularValorCarrinho();
        }
        internal void AdicionarItem(CarrinhoItem item)
        {

            item.AssociarCarrinho(Id);
            if (CarrinhoItemExistente(item))
            {
                var itemExistente = ObterPorProdutoId(item.ProductId);
                itemExistente.AdicionarUnidades(item.Quantidade);

                item = itemExistente;
                Itens.Remove(itemExistente);
            }
            Itens.Add(item);

            CalcularValorCarrinho();
        }
        internal bool EhValido()
        {
            var erros = Itens.SelectMany(x => new CarrinhoItem.ItemCarrinhoValidation().Validate(x).Errors).ToList();
            erros.AddRange(new CarrinhoClienteValidation().Validate(this).Errors);
            ValidationResult = new ValidationResult(erros);

            return ValidationResult.IsValid;
        }
        public class CarrinhoClienteValidation : AbstractValidator<CarrinhoCliente>
        {
            public CarrinhoClienteValidation()
            {
                RuleFor(c => c.ClienteId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Cliente nao reconhecido");

                RuleFor(c => c.Itens.Count)
                    .GreaterThan(0)
                    .WithMessage("O carrinho nao possui itens");

                RuleFor(c => c.ValorTotal)
                    .GreaterThan(0)
                    .WithMessage("O valor total do carrinho precisa ser maior que 0");
            }
        }
    }
}
