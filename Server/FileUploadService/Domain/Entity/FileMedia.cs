using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP.FileService.Infrastructure;

namespace TAP.FileService.Domain.Entity
{
    /// <summary>
    /// 文件物理介质
    /// </summary>
    public class FileMedia : IEntity<string>
    {
        public string Id { get; set; }

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
    }
}