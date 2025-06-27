using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly ILogger<ContaCorrenteController> _logger;
        private readonly DapperContext _dapperContext;
        private readonly ContaCorrenteRepository _contaCorrenteRepository;
        private readonly MovimentoRepository _movimentoRepository;
        private readonly IdempotenciaRepository _idempotenciaRepository;

        public ContaCorrenteController(ILogger<ContaCorrenteController> logger, DapperContext dapperContext)
        {
            _logger = logger;
            _dapperContext = dapperContext;
            _contaCorrenteRepository = new ContaCorrenteRepository(_dapperContext);
            _movimentoRepository = new MovimentoRepository(_dapperContext);
            _idempotenciaRepository = new IdempotenciaRepository(_dapperContext);
        }

        [HttpPost]
        [HttpGet(Name = "PostMovimentarConta")]
        public IActionResult Post(string identificacaoRequisicao, string contaId, decimal valor, string tipoMovimentacao)
        {
            if (string.IsNullOrEmpty(identificacaoRequisicao))
            {
                return BadRequest(new { Erro = "Id da requisição inválida."});
            }
            
            if (string.IsNullOrEmpty(contaId))
            {
                return BadRequest(new { Erro = "Id da conta inválido."});
            }

            if (tipoMovimentacao != "D" && tipoMovimentacao != "C")
            {
                return BadRequest(new { Erro = "Tipo de movimentação inválida."});
            }

            Movimento mov = new()
            {
                IdContaCorrente = contaId,
                DataMovimento = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                TipoMovimento = tipoMovimentacao,
                Valor = valor
            };

            _movimentoRepository.Inserir(mov);

            return Ok();
        }

        [HttpGet("{id}", Name = "GetObterSaldo")]
        public IActionResult Get(string id)
        {
            ContaCorrente? conta = _contaCorrenteRepository.ObterPorId(id);
            if (conta == null)
            {
                return BadRequest(new { Erro = "Não foi possível encontrar a conta." });
            }
            
            if (!conta.Ativo)
            {
                return BadRequest(new { Erro = "Não é possivel retornar o saldo de contas inativas." });
            }

            decimal saldo = _movimentoRepository.SomarPorConta(id);

            return Ok(new { Saldo = saldo });
        }
    }
}