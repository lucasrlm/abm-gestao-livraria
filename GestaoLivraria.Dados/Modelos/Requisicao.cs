namespace GestaoLivraria.Dados.Modelos
{
    public class Requisicao<T>
    {
        public int? Deslocamento { get; set; }
        public int? TamanhoPagina { get; set; }
        public int? PropriedadeOrdenacao { get; set; }
        public bool OrdemDecrescente { get; set; }
        public T Filtros { get; set; }
    }
}
