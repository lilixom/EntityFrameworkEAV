using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP.FileService.Domain.Entity;
using TAP.FileService.DTO;

namespace TAP.FileService.Domain.Service
{
    public interface IFileMediaService
    {
        FileMetadata SaveFile(FileDTO file);

        FileMetadata DelFile(string id);

        FileMedia GetFile(string id);

        FileMetadata CopyFile(string srcFileId, FileDTO dstFile);
    }
}