using transacao_cartao_api.Entities;

namespace GestaoLivraria.Dados.Entidades
{
    public class Pagamento
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public int UserId { get; set; }
        public Card Card { get; set; }
    }
}
