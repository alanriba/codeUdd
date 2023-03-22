using System;
using System.Threading.Tasks;

namespace CodeUdd.Contract
{
    public interface IFeriadoService
    {
        Task<bool> VerificarFeriado(DateTime fecha);
    }
}
