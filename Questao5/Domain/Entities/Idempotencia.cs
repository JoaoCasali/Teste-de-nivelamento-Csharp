namespace Questao5.Domain.Entities
{
    public class Idempotencia
    {
        public string ChaveIdempotencia { get; set; } = null!;
        public string Requisicao { get; set; } = null!;
        public string Resposta { get; set; } = null!;
    }
}
