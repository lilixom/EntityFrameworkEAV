using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP.FileService.Domain;
using TAP.FileService.Domain.Entity;

namespace TAP.FileService.DTO
{
    public class FileDTO
    {
        public FileDTO()
        {
            this.Tags = new List<FileTag>();
        }

        //Status
        public string Id { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// 资源大小
        /// </summary>
        public long ResourceSize { get; set; }

        /// <summary>
        /// 扩张名//查询
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 修改号
        /// </summary>
        public long RevisionNo { get; set; }

        /// <summary>
        /// 介质ID
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 逻辑目录
        /// </summary>
        public string CatalogUri { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public virtual ICollection<FileTag> Tags { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// 相对路径
        /// </summary>
        public string RelativePath { get; set; }

        /// <summary>
        /// 绝对路径
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// MD5Check
        /// </summary>
        public string CheckSum { get; set; }

        /// <summary>
        /// 文件流
        /// </summary>
        public Stream FileStream { get; set; }

        public PropertyCollection Propertys { get; set; }
    }
}