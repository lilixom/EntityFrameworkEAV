using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP.FileService.Domain.Entity;

namespace TAP.FileService.Domain
{
    internal class FileFactory
    {
        public static FileMetadata CreateFile(
            string id,
            string name,
            string catalog,
            DateTime createTime,
            string extension,
            string mediaId,
            DateTime modifiedTime,
            string modifier,
            string owner,
            long size,
            PropertyCollection propertys)
        {
            var metadata = new FileMetadata
            {
                CatalogUri = catalog,
                CreatedTime = createTime,
                Extension = extension,
                Id = id,
                IsDel = false,
                MediaId = mediaId,
                LastModifiedTime = modifiedTime,
                LastModifier = modifier,
                Owner = owner,
                ResourceName = name,
                ResourceSize = size,
                Revision = 1,
                Propertys = propertys
            };
            if (string.IsNullOrEmpty(metadata.Id))
            {
                metadata.Id = Guid.NewGuid().ToString().ToUpper();
                foreach (var ppt in metadata.Propertys)
                {
                    ppt.FileId = metadata.Id;
                }
            }
            return metadata;
        }

        public static FileMedia CreateFileMeida(Stream stream, string checkSum, string fullPath, string relativePath)
        {
            return new FileMedia
            {
                CheckSum = checkSum,
                FileStream = stream,
                FullPath = fullPath,
                RelativePath = relativePath,
                Id = Guid.NewGuid().ToString().ToUpper()
            };
        }
    }
}