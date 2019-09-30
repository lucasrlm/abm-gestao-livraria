using GestaoLivraria.Dados.Entidades;
using GestaoLivraria.Dados.Modelos;
using GestaoLivraria.Dados.Modelos.CriarComentario;
using GestaoLivraria.Dados.Modelos.CriarLivro;
using GestaoLivraria.Dados.Modelos.ListarComentarios;
using GestaoLivraria.Dados.Modelos.ListarLivros;
using GestaoLivraria.Repositorio;
using GestaoLivraria.Util;
using GestaoLivraria.Util.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace GestaoLivraria.Negocio
{
    public class LivroNegocio
    {
        private readonly LivroRepositorio _livroRepositorio = new LivroRepositorio();

        private readonly ComentarioRepositorio _comentarioRepositorio = new ComentarioRepositorio();

        public IEnumerable<ListarLivrosRetorno> ListarLivros(Requisicao<ListarLivrosRequisicao> requisicao)
        {
            requisicao.ValidarParametros();
            ValidarParametrosListarLivros(requisicao.Filtros);

            var livrosEntidade = _livroRepositorio.ListarLivros(requisicao);

            var comentariosEntidade = _comentarioRepositorio.ListarComentarios(new Requisicao<ListarComentariosRequisicao>()
            {
                Filtros = new ListarComentariosRequisicao()
                {
                    LivroId = requisicao.Filtros.Id,
                    TextoAutocomplete = requisicao.Filtros.TextoAutocomplete
                }
            });

            var itens = new List<ListarLivrosRetorno>();

            foreach(var livro in livrosEntidade)
            {
                itens.Add(new ListarLivrosRetorno()
                {
                    Livro = livro,
                    Comentarios = comentariosEntidade.Where(c => c.LivroId == livro.Id)
                });
            }

            return itens;            
        }

        public Livro CriarLivro(CriarLivroRequisicao requisicao)
        {
            return new Livro
            {
                Id = 1,
                Nome = requisicao.Nome
            };
        }

        public Comentario CriarComentario(int livroId, CriarComentarioRequisicao requisicao)
        {
            ValidarParametrosCriarComentario(livroId);

            return new Comentario
            {
                Id = 1,
                LivroId = livroId,
                Texto = requisicao.Texto,
                UsuarioNome = requisicao.UsuarioNome
            };
        }

        public void ValidarParametrosListarLivros(ListarLivrosRequisicao requisicao)
        {
            if (requisicao.Id <= 0)
                throw new BadRequestException(string.Format(Constantes.Erros.PARAMETRO_INVALIDO, nameof(requisicao.Id)));
        }

        public void ValidarParametrosCriarComentario(int livroId)
        {
            if (livroId <= 0)
                throw new BadRequestException(string.Format(Constantes.Erros.PARAMETRO_INVALIDO, nameof(livroId)));

        }
    }
}
