using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP.FileService.Domain.Entity;
using TAP.FileService.Infrastructure;

namespace TAP.FileService.Domain.Repository
{
    public interface IFileMediaRepository : IRepository<FileMedia, String>
    {
    }
}