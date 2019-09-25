using System;

namespace GestaoLivraria.Util.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string erro) : base(erro) { }
    }
}
