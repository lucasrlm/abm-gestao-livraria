using GestaoLivraria.Dados.Entidades;
using GestaoLivraria.Dados.Modelos;
using GestaoLivraria.Mock;
using System.Collections.Generic;
using System.Linq;

namespace GestaoLivraria.Repositorio
{
    public class LivroRepositorio
    {
        public IEnumerable<Livro> ListarLivros(ListarLivrosRequisicao requisicao)
        {
            var tabelaLivros = new LivroMock().ListarLivros();

            var livros = new List<Livro>(); 

            if (requisicao.Id != null)
                livros = tabelaLivros.Where(l => l.Id == requisicao.Id).ToList();

            if (string.IsNullOrEmpty(requisicao.TextoAutocomplete))
                livros = livros.Where(l => l.Nome.Contains(requisicao.TextoAutocomplete) || l.Id.ToString().Contains(requisicao.TextoAutocomplete)).ToList();

            return livros;
        }
    }
}
