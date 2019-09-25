using System;

namespace GestaoLivraria.Util.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string erro) : base(erro) { }
    }
}
