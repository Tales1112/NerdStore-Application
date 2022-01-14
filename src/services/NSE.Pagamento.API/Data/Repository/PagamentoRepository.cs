﻿using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using NSE.Pagamentos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Pagamentos.API.Data.Repository
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly PagamentosContext _context;

        public PagamentoRepository(PagamentosContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public void AdicionarPagamento(Pagamento pagamento)
        {
            _context.Pagamentos.Add(pagamento);
        }
        public void AdicionarTransacao(Transacao transacao)
        {
            _context.Transacoes.Add(transacao);
        }
        public async Task<Pagamento> ObterPagamentoPorPedidoId(Guid PedidoId)
        {
            return await _context.Pagamentos.AsNoTracking()
                .FirstOrDefaultAsync(p => p.PedidoId == PedidoId);
        }
        public async Task<IEnumerable<Transacao>> ObterTransacoesPorPedidoId(Guid pedidoId)
        {
            return await _context.Transacoes.AsNoTracking()
                .Where(t => t.Pagamento.PedidoId == pedidoId).ToListAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}