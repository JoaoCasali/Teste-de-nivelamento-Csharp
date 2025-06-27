using Dapper;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database
{
    public sealed class IdempotenciaRepository
    {
        private readonly DapperContext _context;

        public IdempotenciaRepository(DapperContext context)
            => _context = context;

        public async Task Inserir(Idempotencia registro)
        {
            const string sql = @"
            INSERT INTO Idempotencia
                (chave_idempotencia,
                 requisicao,
                 resultado)
            VALUES
                (@ChaveIdempotencia,
                 @Requisicao,
                 @Resposta);
            ";

            using var db = _context.CreateConnection();
            await db.ExecuteAsync(sql, registro);
        }

        public string? ObterRespostaPorChave(string chaveIdempotencia)
        {
            const string sql = @"
            SELECT Resposta
            FROM   Idempotencia
            WHERE  ChaveIdempotencia = @ChaveIdempotencia;
            ";

            using var db = _context.CreateConnection();
            return db.QueryFirstOrDefault<string?>(
                         sql,
                         new { ChaveIdempotencia = chaveIdempotencia });
        }
    }
}
