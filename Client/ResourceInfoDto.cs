using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    [MessageContract]
    public class ResoureInfoDto : IDisposable
    {
        private Stream _fileContent;

        public ResoureInfoDto()
        {
            _fileContent = new MemoryStream();
        }

        /// <summary>
        /// 文件名全称
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string FileName { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public long FileSize { get; set; }

        /// <summary>
        /// 浏览目录
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string Catalog { get; set; }

        /// <summary>
        /// 版本标签
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string VersionTag { get; set; }

        /// <summary>
        /// 文件内容
        /// </summary>
        [MessageBodyMember(Order = 1)]
        public Stream FileContent
        {
            get { return this._fileContent; }
            set { this._fileContent = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 归属人
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public String Owner { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public Dictionary<String, String> KeyTags { get; set; }

        public void Dispose()
        {
            // close stream when the contract instance is disposed. this ensures that stream is closed when file download is complete, since download procedure is handled by the client and the stream must be closed on server.
            // thanks Bhuddhike! http://blogs.thinktecture.com/buddhike/archive/2007/09/06/414936.aspx
            if (_fileContent != null)
            {
                _fileContent.Close();
                _fileContent = null;
            }
        }
    }

    [MessageContract]
    public class ResoureMetaDataDto
    {
        /// <summary>
        /// 文件Id
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string FileId { get; set; }

        /// <summary>
        /// 文件名全称
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string FileName { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public long FileSize { get; set; }

        /// <summary>
        /// 浏览目录
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string Catalog { get; set; }

        /// <summary>
        /// 版本标签
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string VersionNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 归属人
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public String Owner { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public Dictionary<String, String> KeyTags { get; set; }
    }
}