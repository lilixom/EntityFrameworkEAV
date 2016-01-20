using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP.Assertion;
using TAP.FileService.Domain.Entity;
using TAP.FileService.Domain.Repository;
using TAP.FileService.DTO;
using TAP.FileService.Helper;
using TAP.FileService.Infrastructure;

namespace TAP.FileService.Domain.Service.Impl
{
    internal class FileMediaService : IFileMediaService
    {
        private IFileMetadataRepository _fileMetadataRepo;
        private IFileMediaRepository _fileMediaRepo;
        private IUnitOfWork _unitOfWork;

        public FileMediaService(IFileMetadataRepository fileMetadataRepo,
           IFileMediaRepository fileMediaRepo,
            IUnitOfWork unitOfWork)
        {
            _fileMetadataRepo = fileMetadataRepo;
            _fileMediaRepo = fileMediaRepo;
            _unitOfWork = unitOfWork;
        }

        public FileMetadata SaveFile(FileDTO file)
        {
            var basePath = @"D:\";
            var chkSum = FileHelper.MD5File(file.FileStream);
            bool isAdd = true;
            var media = _fileMediaRepo.DbSet().Where(c => c.CheckSum == chkSum).FirstOrDefault();
            //复制相同文件重复上次
            if (media == null)
            {
                var relativePath = file.CreatedTime.ToString("yyyyMMdd") + @"\";
                var fullPath = basePath + relativePath + Guid.NewGuid();
                media = FileFactory.CreateFileMeida(file.FileStream, chkSum, fullPath, relativePath);
                _fileMediaRepo.Add(media);
            }
            var metaData = _fileMetadataRepo.DbSet().Where(c => c.Id == file.Id).FirstOrDefault();
            if (metaData == null)
            {
                var extension = file.ResourceName.Substring(file.ResourceName.LastIndexOf("."));
                metaData = FileFactory.CreateFile(
                    file.Id,
                    file.ResourceName,
                    file.CatalogUri,
                    file.CreatedTime,
                    extension,
                    media.Id,
                    file.CreatedTime,
                    file.Owner,
                    file.Owner,
                    file.ResourceSize,
                    file.Propertys
                    );
            }
            else
            {
                isAdd = false;
                metaData.Revision++;
                metaData.LastModifiedTime = file.CreatedTime;
                metaData.LastModifier = file.Owner;
                metaData.ResourceName = file.ResourceName;
                metaData.CatalogUri = file.CatalogUri;
                metaData.Extension = file.ResourceName.Substring(file.ResourceName.LastIndexOf("."));
            }

            var version = new FileVersion
            {
                Remark = "",
                Id = Guid.NewGuid().ToString().ToUpper(),
                ModifiedTime = metaData.LastModifiedTime,
                Modifier = metaData.LastModifier,
                Revision = metaData.Revision,
                FileMetadata = metaData,
                FileId = metaData.Id,
                FileInfor = JsonConvert.SerializeObject(metaData)
            };
            metaData.Versions.Add(version);

            if (isAdd)
            {
                _fileMetadataRepo.Add(metaData);
            }

            var i = _unitOfWork.Commit();

            return metaData;
        }

        public FileMedia GetFile(string id)
        {
            var media = _fileMediaRepo.Get(id);

            return media;
        }

        public FileMetadata DelFile(string id)
        {
            var metadata = _fileMetadataRepo.Get(id).ShouldNotDefault("不存在资源ID{0}", id);
            _fileMetadataRepo.Remove(metadata);
            _unitOfWork.Commit();
            return metadata;
        }

        public FileMetadata CopyFile(string srcFileId, FileDTO dstFile)
        {
            var sourceAttach = _fileMediaRepo.Get(srcFileId).ShouldNotDefault("无法找到ID = {0} 的附件记录", srcFileId);

            if (String.IsNullOrEmpty(dstFile.Id))
            {
                dstFile.Id = Guid.NewGuid().ToString();
            }
            return SaveFile(dstFile);
        }
    }
}