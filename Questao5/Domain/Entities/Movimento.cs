namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public string IdMovimento { get; set; } = null!;
        public string IdContaCorrente { get; set; } = null!;
        public string DataMovimento { get; set; } = null!;
        public string TipoMovimento { get; set; } = null!;
        public decimal Valor { get; set; }
    }
}
