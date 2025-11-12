using Microsoft.AspNetCore.Mvc;

namespace gs_hybrid.hybrid_api.controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersControllerV2 : ControllerBase
    {
        /// <summary>
        /// Retorna a lista de usuários com formato diferente (versão 2).
        /// </summary>
        [HttpGet]
        public IActionResult GetUsersV2()
        {
            var users = new[]
            {
                new { Id = 1, NomeCompleto = "Usuário da Versão 2", Cargo = "Administrador" },
                new { Id = 2, NomeCompleto = "Maria Teste V2", Cargo = "Analista" }
            };

            return Ok(new
            {
                Versao = "2.0",
                Mensagem = "Esta é a nova versão da API de usuários",
                Usuarios = users
            });
        }
    }
}
