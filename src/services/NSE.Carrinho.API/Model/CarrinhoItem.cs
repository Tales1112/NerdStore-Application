﻿using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace NSE.Carrinho.API.Model
{
    public class CarrinhoItem
    {
        public CarrinhoItem()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }
        public Guid CarrinhoId { get; set; }
        [JsonIgnore]
        public CarrinhoCliente CarrinhoCliente { get; set; }

        internal void AssociarCarrinho(Guid carrinhoId)
        {
            CarrinhoId = carrinhoId;
        }
        internal decimal CalcularValor()
        {
            return Quantidade * Valor;
        }
        internal void AtualizarUnidades(int unidades)
        {
            Quantidade = unidades;
        }
        internal void AdicionarUnidades(int unidades)
        {
            Quantidade += unidades;
        }
        internal bool EhValido()
        {
            return new ItemCarrinhoValidation().Validate(this).IsValid;
        }
        public class ItemCarrinhoValidation : AbstractValidator<CarrinhoItem>
        {
            public ItemCarrinhoValidation()
            {
                RuleFor(c => c.ProductId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do produto Invalido");

                RuleFor(c => c.Nome)
                    .NotEmpty()
                    .WithMessage("O nome do produto nao foi informado");

                RuleFor(c => c.Quantidade)
                    .GreaterThan(0)
                    .WithMessage(item => $"A quantidade minima para o {item.Nome} e 1");

                RuleFor(c => c.Quantidade)
                    .LessThan(5)
                    .WithMessage(item => $"A quantidade maxima do {item.Nome} e 5");

                RuleFor(c => c.Valor)
                   .LessThan(0)
                   .WithMessage(item => $"O valor do {item.Nome} item precisa ser maior que 0");
            }
        }
    }
}
