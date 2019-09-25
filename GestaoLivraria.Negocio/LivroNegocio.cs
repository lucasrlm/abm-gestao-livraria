using GestaoLivraria.Dados.Modelos;
using GestaoLivraria.Dados.Modelos.ListarComentarios;
using GestaoLivraria.Dados.Modelos.ListarLivros;
using GestaoLivraria.Repositorio;
using System.Collections.Generic;
using System.Linq;

namespace GestaoLivraria.Negocio
{
    public class LivroNegocio
    {
        private readonly LivroRepositorio _livroRepositorio = new LivroRepositorio();

        private readonly ComentarioRepositorio _comentarioRepositorio = new ComentarioRepositorio();

        public Retorno<ListarLivrosRetorno> ListarLivros(Requisicao<ListarLivrosRequisicao> requisicao)
        {
            var livrosEntidade = _livroRepositorio.ListarLivros(requisicao);

            var comentariosEntidade = _comentarioRepositorio.ListarComentarios(new Requisicao<ListarComentariosRequisicao>()
            {
                Filtros = new ListarComentariosRequisicao()
                {
                    LivroId = requisicao.Filtros.Id,
                    TextoAutocomplete = requisicao.Filtros.TextoAutocomplete
                }
            });

            var retorno = new Retorno<ListarLivrosRetorno>();
            var itens = new List<ListarLivrosRetorno>();

            foreach(var livro in livrosEntidade)
            {
                itens.Add(new ListarLivrosRetorno()
                {
                    Livro = livro,
                    Comentarios = comentariosEntidade.Where(c => c.LivroId == livro.Id)
                });
            }

            retorno.Itens = itens;

            return retorno;
        }
    }
}
