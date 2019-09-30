using GestaoLivraria.Util;
using GestaoLivraria.Util.Exceptions;

namespace GestaoLivraria.Dados.Modelos
{
    public class Requisicao<T>
    {
        public int? Deslocamento { get; set; }
        public int? TamanhoPagina { get; set; }
        public int? PropriedadeOrdenacao { get; set; }
        public bool OrdemDecrescente { get; set; }
        public T Filtros { get; set; }

        public void ValidarParametros()
        {
            if (Deslocamento.HasValue && Deslocamento <= 0)
                throw new BadRequestException(string.Format(Constantes.Erros.PARAMETRO_INVALIDO, nameof(Deslocamento)));
            if (TamanhoPagina.HasValue && TamanhoPagina <= 0)
                throw new BadRequestException(string.Format(Constantes.Erros.PARAMETRO_INVALIDO, nameof(TamanhoPagina)));
            if (PropriedadeOrdenacao.HasValue && PropriedadeOrdenacao <= 0)
                throw new BadRequestException(string.Format(Constantes.Erros.PARAMETRO_INVALIDO, nameof(PropriedadeOrdenacao)));
        }
    }
}
