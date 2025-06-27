using Dapper;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database
{
    public sealed class MovimentoRepository
    {
        private readonly DapperContext _context;

        public MovimentoRepository(DapperContext context)
            => _context = context;

        public void Inserir(Movimento movimento)
        {
            const string sql = @"
            INSERT INTO Movimento
                (idcontacorrente,
                 datamovimento,
                 tipomovimento,
                 valor)
            VALUES
                (@IdContaCorrente,
                 @DataMovimento,
                 @TipoMovimento,
                 @Valor);
            ";

            using var db = _context.CreateConnection();
            db.Execute(sql, movimento);
        }

        public decimal SomarPorConta(string idContaCorrente)
        {
            const string sql = @"
            SELECT
                COALESCE(
                SUM(
                    CASE
                        WHEN tipomovimento = 'D' THEN - valor
                        ELSE valor                             
                    END
                ), 0
            )
            FROM Movimento
            WHERE idcontacorrente = @IdContaCorrente;
            ";

            using var db = _context.CreateConnection();
            return db.ExecuteScalar<decimal>(sql, new { IdContaCorrente = idContaCorrente });
        }
    }
}
