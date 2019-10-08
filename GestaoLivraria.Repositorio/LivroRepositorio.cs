using GestaoLivraria.Dados.Entidades;
using GestaoLivraria.Dados.Modelos;
using GestaoLivraria.Dados.Modelos.ListarLivros;
using GestaoLivraria.Mock;
using System.Collections.Generic;
using System.Linq;

namespace GestaoLivraria.Repositorio
{
    public class LivroRepositorio
    {
        public IEnumerable<Livro> ListarLivros(Requisicao<ListarLivrosRequisicao> requisicao)
        {
            var tabelaLivros = new LivroMock().ListarLivros();

            var livros = tabelaLivros.ToList();
            
            if(requisicao != null && requisicao.Filtros != null)
            {
                if (requisicao.Filtros.Id.HasValue)
                    livros = tabelaLivros.Where(l => l.Id == requisicao.Filtros.Id).ToList();

                else if (string.IsNullOrEmpty(requisicao.Filtros.TextoAutocomplete))
                    livros = livros.Where(l => l.Nome.Contains(requisicao.Filtros.TextoAutocomplete) || 
                    l.Id.ToString().Contains(requisicao.Filtros.TextoAutocomplete)).ToList();
            }

            return livros;
        }
    }
}
