using System.Collections.Generic;
using System.Linq;

namespace GestaoLivraria.Dados.Modelos
{
    public class Retorno<T>
    {
        public int Pagina { get; set; }
        public int TamanhoPagina { get; set; }
        public int TotalItens { get; set; }
        public IEnumerable<T> Itens { get; set; }

        public Retorno<T> RetornarPaginadoOrdenado(int? propriedadeOrdenacao, bool ordemDescendente, int? deslocamento, int? tamanhoPagina)
        {
            var retorno = this;

            if (propriedadeOrdenacao.HasValue && !ordemDescendente)
            {
                retorno.Itens = retorno.Itens.OrderBy(r => r.GetType().GetProperties()[propriedadeOrdenacao.Value]);
            }
            else if (propriedadeOrdenacao.HasValue && ordemDescendente)
            {
                retorno.Itens = retorno.Itens.OrderByDescending(r => r.GetType().GetProperties()[propriedadeOrdenacao.Value]);
            }
            if (deslocamento.HasValue)
                retorno.Itens = retorno.Itens.Skip(deslocamento.Value);
            if (tamanhoPagina.HasValue)
                retorno.Itens = retorno.Itens.Take(tamanhoPagina.Value);

            retorno.TotalItens = retorno.Itens.Count();
            retorno.Pagina = tamanhoPagina.HasValue ? (retorno.TotalItens / tamanhoPagina.Value) + 1 : 1;
            retorno.TamanhoPagina = retorno.TotalItens;

            return retorno;
        }
    }
}
