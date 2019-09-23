namespace GestaoLivraria.Dados.Entidades
{
    public class PedidoDetalhes
    {
        public int Id { get; set; }
        public int PedidoCabecalhoId { get; set; }
        public int LivroId { get; set; }
    }
}
