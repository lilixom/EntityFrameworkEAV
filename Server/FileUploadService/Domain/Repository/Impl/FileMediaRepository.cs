using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP.FileService.Domain.Entity;
using TAP.FileService.Helper;
using TAP.FileService.Infrastructure;
using TAP.FileService.Infrastructure.Impl.EF;

namespace TAP.FileService.Domain.Repository.Impl
{
    internal class FileMediaRepository : EFRepository<FileMedia, string>, IFileMediaRepository
    {
        private object _lockObj;

        public FileMediaRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)

        { }

        public new void Add(FileMedia media)
        {
            base.Add(media);
            SaveFile(media);
        }

        public new void Remove(FileMedia item)
        {
            base.Remove(item);
        }

        public new FileMedia Get(string id)
        {
            var media = base.Get(id);
            media.FileStream = GetFile(media);
            return media;
        }

        private void EnsureDir(string fullPath)
        {
            var path = Path.GetDirectoryName(fullPath);
            if (path != null && !Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private Stream GetFile(FileMedia item)
        {
            if (!File.Exists(item.FullPath))
            {
                return null;
            }
            return File.OpenRead(item.FullPath);
        }

        /// <summary>
        /// 暂时不做断点续传
        /// </summary>
        /// <param name="item"></param>
        private void SaveFile(FileMedia item)
        {
            EnsureDir(item.FullPath);
            string filePath = item.FullPath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (FileStream writeStream = File.OpenWrite(filePath))
            {
                item.FileStream.CopyTo(writeStream);
            }
            //lock (_lockObj)
            //{
            //    string tempFile = Path.GetTempFileName();
            //    if (File.Exists(filePath))
            //    {
            //        File.Delete(filePath);
            //    }
            //    int chunkSize = 2048;
            //    byte[] buffer = new byte[chunkSize];
            //    //using (FileStream writeStream = File.OpenWrite(tempFile))
            //    //{
            //    //    do
            //    //    {
            //    //        int bytesRead = item.FileStream.Read(buffer, 0, chunkSize);
            //    //        if (bytesRead == 0) break;
            //    //        writeStream.Write(buffer, 0, bytesRead);
            //    //    } while (true);
            //    //    writeStream.Close();
            //    //}
            //    File.Move(tempFile, filePath);
            //}
        }
    }
}