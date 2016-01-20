using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TAP.FileService.Domain.Entity;
using TAP.FileService.Helper;
using TAP.FileService.Infrastructure;
using TAP.FileService.Infrastructure.Impl.EF;

namespace TAP.FileService.Domain.Repository.Impl
{
    public class FileMetadataRepository : EFRepository<FileMetadata, string>, IFileMetadataRepository
    {
        public FileMetadataRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
    }
}