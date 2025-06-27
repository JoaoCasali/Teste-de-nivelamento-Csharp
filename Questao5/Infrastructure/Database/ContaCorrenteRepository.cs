using Dapper;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database
{
    public sealed class ContaCorrenteRepository
    {
        private readonly DapperContext _context;

        public ContaCorrenteRepository(DapperContext context)
            => _context = context;

        public ContaCorrente? ObterPorId(string idContaCorrente)
        {
            const string sql = @"
            SELECT
                idcontacorrente,
                numero,
                nome,
                ativo
            FROM ContaCorrente 
            WHERE IdContaCorrente = @Id;
            ";

            using var db = _context.CreateConnection();
            return db.QueryFirstOrDefault<ContaCorrente?>(sql, new { Id = idContaCorrente });
        }
    }
}
