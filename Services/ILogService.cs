using System;

namespace LaToxicaCRUD.Services
{
    public interface ILogService
    {
        void RegistrarError(string origen, Exception ex);
    }
}
