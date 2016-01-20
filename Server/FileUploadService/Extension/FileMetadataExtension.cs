using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP.FileService.Domain;
using TAP.FileService.Domain.Entity;
using TAP.FileService.DTO;

namespace TAP.FileService.Extension
{
    public static class FileMetadataExtension
    {
        public static ResoureMetadataMessage ConverntToResoureMetaDataMsg(this FileMetadata metadata)
        {
            if (metadata == null)
            {
                return null;
            }
            var dto = new ResoureMetadataMessage
            {
                Catalog = metadata.CatalogUri,
                CreatedTime = metadata.CreatedTime,
                FileId = metadata.Id,
                FileName = metadata.ResourceName,
                FileSize = metadata.ResourceSize,
                //KeyTags = metadata.Tags,
                Owner = metadata.Owner,
                RevisionNo = metadata.Revision
            };

            return dto;
        }

        public static ResoureMetadataDTO ConverntToResoureMetaDataDto(this FileMetadata metadata)
        {
            if (metadata == null)
            {
                return null;
            }
            var dto = new ResoureMetadataDTO
            {
                Catalog = metadata.CatalogUri,
                CreatedTime = metadata.CreatedTime,
                FileId = metadata.Id,
                FileName = metadata.ResourceName,
                FileSize = metadata.ResourceSize,
                //KeyTags = metadata.Tags,
                Owner = metadata.Owner,
                RevisionNo = metadata.Revision
            };

            return dto;
        }

        public static IEnumerable<ResoureMetadataDTO> ConvertToResoureMetaDataDto(this IEnumerable<FileMetadata> files)
        {
            if (files == null)
                return null;
            var dtos = new List<ResoureMetadataDTO>();
            foreach (var file in files)
            {
                dtos.Add(file.ConverntToResoureMetaDataDto());
            }
            return dtos;
        }
    }

    public static class FileVersionExtension
    {
        public static IEnumerable<ResoureMetadataDTO> ConvertToResoureMetaDataDto(this IEnumerable<FileVersion> versions)
        {
            if (versions == null)
                return null;
            var dtos = new List<ResoureMetadataDTO>();
            foreach (var ver in versions)
            {
                ver.FileMetadata = JsonConvert.DeserializeObject<FileMetadata>(ver.FileInfor);
                dtos.Add(ver.FileMetadata.ConverntToResoureMetaDataDto());
            }
            return dtos;
        }
    }

    public static class ResoureInfoDtoExtension
    {
        public static FileDTO ConvertToFileDTO(this ResoureMessage resoure)
        {
            if (resoure == null)
            {
                return null;
            }
            FileDTO dto = new FileDTO
            {
                CatalogUri = resoure.Catalog,
                CreatedTime = resoure.CreatedTime,
                FileStream = resoure.FileContent,
                Id = resoure.FileId,
                Owner = resoure.Owner,
                ResourceName = resoure.FileName,
                ResourceSize = resoure.FileSize,
                Propertys = new PropertyCollection()
            };
            foreach (var pair in resoure.KeyTags)
            {
                FileMetadataEAVProperty property = new FileMetadataEAVProperty
                {
                    FileId = resoure.FileId,
                    Name = pair.Key,
                    Value = pair.Value
                };
                dto.Propertys.Add(property);
            }
            return dto;
        }

        public static FileDTO ConvertToFileDTO(this ResoureMetadataDTO metadata)
        {
            if (metadata == null)
            {
                return null;
            }
            FileDTO dto = new FileDTO
            {
                CatalogUri = metadata.Catalog,
                CreatedTime = metadata.CreatedTime,
                Id = metadata.FileId,
                Owner = metadata.Owner,
                ResourceName = metadata.FileName,
                ResourceSize = metadata.FileSize
            };
            foreach (var pair in metadata.KeyTags)
            {
                FileMetadataEAVProperty property = new FileMetadataEAVProperty
                {
                    Name = pair.Key,
                    Value = pair.Value
                };
            }
            return dto;
        }
    }
}