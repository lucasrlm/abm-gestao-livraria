namespace GestaoLivraria.Dados.Entidades
{
    public class Pagamento
    {
        public int Id { get; set; }
        public int Valor { get; set; }
        public string Moeda { get; set; }
        public int IdUsuario { get; set; }
        public Cartao Cartao { get; set; }
    }
}
