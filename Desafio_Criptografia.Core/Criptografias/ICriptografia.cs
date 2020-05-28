using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio_Criptografia.Core.Criptografias
{
    public interface ICriptografia<T>
    {
        T Criptografar(T obj);
        T Descriptografar(T obj);
    }
}
