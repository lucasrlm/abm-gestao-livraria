using GestaoLivraria.Dados.Entidades;
using GestaoLivraria.Dados.Modelos;
using GestaoLivraria.Dados.Modelos.CriarLivro;
using GestaoLivraria.Dados.Modelos.ListarLivros;
using GestaoLivraria.Negocio;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GestaoLivraria.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly LivroNegocio _livroNegocio = new LivroNegocio();

        [HttpGet]
        public ActionResult<Retorno<ListarLivrosRetorno>> Get([FromQuery]Requisicao<ListarLivrosRequisicao> requisicao)
        {
            var retorno = _livroNegocio.ListarLivros(requisicao);

            if (retorno.Itens == null || retorno.Itens.Count() == 0)
                return NoContent();

            return Ok(retorno.RetornarPaginadoOrdenado
                (requisicao.PropriedadeOrdenacao,
                 requisicao.OrdemDescendente,
                 requisicao.Deslocamento,
                 requisicao.TamanhoPagina));
        }

        [HttpPost]
        public ActionResult<Livro> Post([FromBody] CriarLivroRequisicao requisicao)
        {
            return Ok(new Livro
            {
                Id = 1,
                Nome = requisicao.Nome
            });
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
