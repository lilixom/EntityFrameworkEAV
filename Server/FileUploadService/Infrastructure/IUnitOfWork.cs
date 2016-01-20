using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAP.FileService.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        // Methods
        void RollBack();

        int Commit();
    }
}