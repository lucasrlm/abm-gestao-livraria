using GestaoLivraria.Dados.Entidades;
using GestaoLivraria.Dados.Modelos;
using GestaoLivraria.Dados.Modelos.CriarComentario;
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
            var retorno = new Retorno<ListarLivrosRetorno>()
            {
                Itens = _livroNegocio.ListarLivros(requisicao)
            };

            if (retorno.Itens == null || retorno.Itens.Count() == 0)
                return NoContent();

            return Ok(retorno.RetornarPaginadoOrdenado
                (requisicao.PropriedadeOrdenacao,
                 requisicao.OrdemDecrescente,
                 requisicao.Deslocamento,
                 requisicao.TamanhoPagina));
        }

        [HttpPost]
        public ActionResult<Livro> Post([FromBody]CriarLivroRequisicao requisicao)
        {
            return Ok(_livroNegocio.CriarLivro(requisicao));            
        }

        [HttpPost("{livroId}/Comentarios")]
        public ActionResult<Comentario> PostComentario(int livroId, [FromBody]CriarComentarioRequisicao requisicao)
        {
            return Ok(_livroNegocio.CriarComentario(livroId, requisicao));
        }
    }
}
