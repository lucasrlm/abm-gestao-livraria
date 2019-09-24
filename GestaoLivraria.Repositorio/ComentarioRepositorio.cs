using GestaoLivraria.Dados.Entidades;
using GestaoLivraria.Dados.Modelos;
using GestaoLivraria.Dados.Modelos.ListarComentarios;
using GestaoLivraria.Mock;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GestaoLivraria.Repositorio
{
    public class ComentarioRepositorio
    {
        public IEnumerable<Comentario> ListarComentarios(Requisicao<ListarComentariosRequisicao> requisicao)
        {
            var tabelaComentarios = new ComentarioMock().ListarComentarios();

            var comentarios = new List<Comentario>();

            if (requisicao.Filtros.Id.HasValue)
                comentarios = tabelaComentarios.Where(c => c.Id == requisicao.Filtros.Id.Value).ToList();

            else if (requisicao.Filtros.GetType().GetProperties().Except(new List<PropertyInfo>() { requisicao.Filtros.GetType().GetProperty("Id") }).
                Any(p => p.GetValue(requisicao.Filtros) != null))
            {
                if (requisicao.Filtros.LivroId.HasValue)
                    comentarios = tabelaComentarios.Where(c => c.LivroId == requisicao.Filtros.LivroId.Value).ToList();

                if (!string.IsNullOrEmpty(requisicao.Filtros.TextoAutocomplete))
                    comentarios = tabelaComentarios.Where(c => c.UsuarioNome.Contains(requisicao.Filtros.TextoAutocomplete) ||
                                                               c.Texto.Contains(requisicao.Filtros.TextoAutocomplete)).ToList();
            }

            return comentarios;
        }
    }
}
