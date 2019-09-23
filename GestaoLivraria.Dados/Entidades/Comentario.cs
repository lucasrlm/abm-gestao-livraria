namespace GestaoLivraria.Dados.Entidades
{
    public class Comentario
    {
        public int Id { get; set; }
        public int LivroId { get; set; }
        public string UsuarioNome { get; set; }
        public string Texto { get; set; }
    }
}
