using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP.FileService.Domain.Repository;
using TAP.FileService.Infrastructure;

namespace TAP.FileService.Domain.Entity
{
    /// <summary>
    /// 文件元数据
    /// </summary>
    public class FileMetadata : IEntity<string>
    {
        public FileMetadata()
        {
            this.Versions = new List<FileVersion>();
        }

        public string Id { get; set; }

        public string ResourceName { get; set; }

        public long ResourceSize { get; set; }

        public string Extension { get; set; }

        /// <summary>
        /// 修改号
        /// </summary>
        public long Revision { get; set; }

        /// <summary>
        /// 介质ID
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 逻辑目录
        /// </summary>
        public string CatalogUri { get; set; }

        //public virtual ICollection<FileTag> Tags { get; set; }

        public virtual ICollection<FileVersion> Versions { get; set; }

        public DateTime LastModifiedTime { get; set; }

        public DateTime CreatedTime { get; set; }

        public string LastModifier { get; set; }

        public string Owner { get; set; }

        public bool IsDel { get; set; }

        public virtual PropertyCollection Propertys { get; set; }
    }
}