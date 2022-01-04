using NSE.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.Cliente.API.Models
{
    public interface IClienteRepository : IRepository<Clientes>
    {
        void Adicionar(Clientes cliente);
        void AdicionarEndereco(Endereco endereco);
        Task<IEnumerable<Clientes>> ObterTodos();
        Task<Clientes> ObterPorCpf(string cpf);
        Task<Endereco> ObterEnderecoPorId(Guid id);
    }
}
