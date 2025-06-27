namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        public string IdContaCorrente { get; set; } = null!;
        public int Numero { get; set; }
        public string Nome { get; set; } = null!;
        public bool Ativo { get; set; }
    }
}
