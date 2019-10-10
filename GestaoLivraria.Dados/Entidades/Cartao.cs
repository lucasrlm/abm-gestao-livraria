namespace GestaoLivraria.Dados.Entidades
{
    public class Cartao
    {
        public int Id { get; set; }
        public string Bandeira { get; set; }
        public string NomeProprietario { get; set; }
        public int MesExpiracao { get; set; }
        public int AnoExpiracao { get; set; }
        public int IdUsuario { get; set; }
        public int CodigoSeguranca { get; set; }
    }
}
