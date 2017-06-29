using System;

namespace CryptoManager.Util
{
    public class CryptoManagerException : Exception
    {
        public CryptoManagerException(string mensagem)
            : base(mensagem)
        {
        }

        public CryptoManagerException(string mensagem, Exception excecaoInterna)
           : base(mensagem, excecaoInterna)
        {
        }
    }
}
