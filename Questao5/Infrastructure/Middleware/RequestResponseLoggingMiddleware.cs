using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;
using System.Text;

namespace Questao5.Infrastructure.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly DapperContext _dapperContext;
        private readonly IdempotenciaRepository _repo;

        public RequestResponseLoggingMiddleware(RequestDelegate next, DapperContext dapperContext)
        {
            _next = next;
            _dapperContext = dapperContext;
            _repo = new IdempotenciaRepository(_dapperContext);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            string requestBody;
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            var originalBodyStream = context.Response.Body;

            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context); 

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            var chave = context.Request.Headers["identificacaoRequisicao"].FirstOrDefault()
                        ?? Guid.NewGuid().ToString();

            var registro = new Idempotencia
            {
                ChaveIdempotencia = chave,
                Requisicao = requestBody,
                Resposta = responseText
            };

            await _repo.Inserir(registro);

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
