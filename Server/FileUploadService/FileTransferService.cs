using Microsoft.Practices.Unity;
using System;
using System.IO;
using System.Linq;
using TAP.Assertion;
using TAP.FileService.Domain;
using TAP.FileService.Domain.Entity;
using TAP.FileService.Domain.Repository;
using TAP.FileService.Domain.Repository.Impl;
using TAP.FileService.Domain.Service;
using TAP.FileService.Domain.Service.Impl;
using TAP.FileService.Extension;
using TAP.FileService.Helper;
using TAP.FileService.Infrastructure;
using TAP.FileService.Infrastructure.Impl.EF;

namespace TAP.FileService
{
    public class FileTransferService : IFileTransferService
    {
        private UnityContainer container = new UnityContainer();

        private IFileMetadataRepository _fileMetadataRepo;
        private IFileMediaRepository _fileMediaRepo;
        private IFileMediaService _fileMediaService;

        public FileTransferService()
        {
            _fileMetadataRepo = DependencyResovler.GobalContainer().Resolve<IFileMetadataRepository>();
            _fileMediaRepo = DependencyResovler.GobalContainer().Resolve<IFileMediaRepository>();
            _fileMediaService = DependencyResovler.GobalContainer().Resolve<IFileMediaService>();
        }

        /// <summary>
        /// to do 断点续传下载功能
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public RemoteFileInfo GetFile(GetRequest request)
        {
            var metadata = _fileMetadataRepo.Get(request.Id).ShouldNotDefault("不存在资源ID{0}", request.Id);
            var media = _fileMediaService.GetFile(metadata.Id);
            RemoteFileInfo result = new RemoteFileInfo();

            media.FileStream.Seek(request.ByteStart, SeekOrigin.Begin);
            result.FileName = metadata.ResourceName;
            result.Length = metadata.ResourceSize;
            result.FileByteStream = media.FileStream;
            return result;
        }

        public ResoureMetadataMessage SaveFile(ResoureMessage request)
        {
            var dto = _fileMediaService.SaveFile(request.ConvertToFileDTO());
            return dto.ConverntToResoureMetaDataMsg();
        }

        public System.Collections.Generic.List<ResoureMetadataDTO> GetFileHistoryInfo(string id)
        {
            var metadata = _fileMetadataRepo.Get(id).ShouldNotDefault("不存在资源ID{0}", id);
            return metadata.Versions.ConvertToResoureMetaDataDto().ToList();
        }

        public ResoureMetadataDTO GetFile(string id)
        {
            var metadata = _fileMetadataRepo.Get(id).ShouldNotDefault("不存在资源ID{0}", id);
            return metadata.ConverntToResoureMetaDataDto();
        }

        public Stream GetFileContent(string id)
        {
            var metadata = _fileMetadataRepo.Get(id).ShouldNotDefault("不存在资源ID{0}", id);
            return _fileMediaService.GetFile(metadata.Id).FileStream;
        }

        public void DeleteFile(string id)
        {
            _fileMediaService.DelFile(id);
        }

        public void DeleteFiles(FileCriteria criteria)
        {
            var query = _fileMetadataRepo.DbSet();
            if (criteria.Catalog != null)
            {
                query.Where(w => w.CatalogUri == criteria.Catalog);
            }
            if (criteria.KeyTags.ContainsKey("FlowInstId"))
            {
                if (criteria.KeyTags["FlowInstId"] != null)
                {
                    query.Where(w => w.Propertys.Any(c => c.Name == "FlowInstId" && c.Value == criteria.KeyTags["FlowInstId"]));
                }
            }
            if (criteria.KeyTags.ContainsKey("ProjectId"))
            {
                if (criteria.KeyTags["ProjectId"] != null)
                {
                    query.Where(w => w.Propertys.Any(c => c.Name == "ProjectId" && c.Value == criteria.KeyTags["ProjectId"]));
                }
            }
            if (criteria.KeyTags.ContainsKey("ReferenceId"))
            {
                if (criteria.KeyTags["ReferenceId"] != null)
                {
                    query.Where(w => w.Propertys.Any(c => c.Name == "ReferenceId" && c.Value == criteria.KeyTags["ReferenceId"]));
                }
            }
            foreach (var item in query)
            {
                _fileMediaService.DelFile(item.Id);
            }
        }

        public System.Collections.Generic.List<ResoureMetadataDTO> GetFiles(FileCriteria criteria)
        {
            var query = _fileMetadataRepo.DbSet();
            if (criteria.Catalog != null)
            {
                query.Where(w => w.CatalogUri == criteria.Catalog);
            }
            if (criteria.KeyTags.ContainsKey("FlowInstId"))
            {
                if (criteria.KeyTags["FlowInstId"] != null)
                {
                    query.Where(w => w.Propertys.Any(c => c.Name == "FlowInstId" && c.Value == criteria.KeyTags["FlowInstId"]));
                }
            }
            if (criteria.KeyTags.ContainsKey("ProjectId"))
            {
                if (criteria.KeyTags["ProjectId"] != null)
                {
                    query.Where(w => w.Propertys.Any(c => c.Name == "ProjectId" && c.Value == criteria.KeyTags["ProjectId"]));
                }
            }
            if (criteria.KeyTags.ContainsKey("ReferenceId"))
            {
                if (criteria.KeyTags["ReferenceId"] != null)
                {
                    query.Where(w => w.Propertys.Any(c => c.Name == "ReferenceId" && c.Value == criteria.KeyTags["ReferenceId"]));
                }
            }
            var list = query.ConvertToResoureMetaDataDto().ToList();
            return list;
        }

        public ResoureMetadataDTO CopyFile(string srcId, ResoureMetadataDTO destFile)
        {
            var dto = _fileMediaService.CopyFile(srcId, destFile.ConvertToFileDTO());
            return dto.ConverntToResoureMetaDataDto();
        }
    }
}