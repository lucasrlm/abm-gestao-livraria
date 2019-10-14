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
    /// <summary>
    /// Controller de livros
    /// </summary>
    [Route("v1/livros")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly LivroNegocio _livroNegocio = new LivroNegocio();

        /// <summary>
        /// Lista todos os livros a partir dos filtros passados
        /// </summary>
        /// <param name="requisicao"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Cria um livro
        /// </summary>
        /// <param name="requisicao"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Livro> Post([FromBody]CriarLivroRequisicao requisicao)
        {
            return Ok(_livroNegocio.CriarLivro(requisicao));            
        }

        /// <summary>
        /// Cria um comentário para um livro
        /// </summary>
        /// <param name="livroId"></param>
        /// <param name="requisicao"></param>
        /// <returns></returns>
        [HttpPost("{livroId}/Comentarios")]
        public ActionResult<Comentario> PostComentario(int livroId, [FromBody]CriarComentarioRequisicao requisicao)
        {
            return Ok(_livroNegocio.CriarComentario(livroId, requisicao));
        }
    }
}
