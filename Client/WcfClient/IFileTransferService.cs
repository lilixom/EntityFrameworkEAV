using Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace Client.WcfClient
{
    [ServiceContract]
    public interface IFileTransferService
    {
        [OperationContract]
        ResoureMetadataMessage SaveFile(ResoureMessage request);

        [OperationContract]
        List<ResoureMetadataDTO> GetFileHistoryInfo(string Id);

        [OperationContract]
        ResoureMetadataDTO GetFile(string id);

        [OperationContract]
        Stream GetFileContent(string id);

        [OperationContract]
        void DeleteFile(string id);

        [OperationContract]
        void DeleteFiles(FileCriteria condition);

        /// <summary>
        /// 复制资源
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [OperationContract]
        List<ResoureMetadataDTO> GetFiles(FileCriteria condition);

        /// <summary>
        /// 复制资源
        /// </summary>
        /// <param name="id"></param>
        /// <param name="destFile"></param>
        /// <returns></returns>
        [OperationContract]
        ResoureMetadataDTO CopyFile(string id, ResoureMetadataDTO destFile);

        /// <summary>
        /// to test 断点续传功能
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        RemoteFileInfo GetFile(GetRequest request);
    }

    [MessageContract]
    public class GetRequest
    {
        [MessageBodyMember]
        public string Id;

        [MessageBodyMember]
        public long ByteStart = 0;
    }

    [MessageContract]
    public class RemoteFileInfo : IDisposable
    {
        [MessageHeader(MustUnderstand = true)]
        public string FileName;

        [MessageHeader(MustUnderstand = true)]
        public long Length;

        [MessageBodyMember(Order = 1)]
        public System.IO.Stream FileByteStream;

        public void Dispose()
        {
            if (FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }
    }

    [MessageContract]
    public class ResoureMessage : IDisposable
    {
        private Stream _fileContent;

        public ResoureMessage()
        {
            _fileContent = new MemoryStream();
        }

        /// <summary>
        /// 资源唯一标识
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
        public DateTime CreatedTime { get; set; }

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
            if (_fileContent != null)
            {
                _fileContent.Close();
                _fileContent = null;
            }
        }
    }

    [DataContract(IsReference = true)]
    public class ResoureMetadataDTO
    {
        /// <summary>
        /// 文件Id
        /// </summary>
        [DataMember]
        public string FileId { get; set; }

        /// <summary>
        /// 文件名全称
        /// </summary>
        [DataMember]
        public string FileName { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [DataMember]
        public long FileSize { get; set; }

        /// <summary>
        /// 浏览目录
        /// </summary>
        [DataMember]
        public string Catalog { get; set; }

        /// <summary>
        /// 修订号
        /// </summary>
        [DataMember]
        public long RevisionNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 归属人
        /// </summary>
        [DataMember]
        public String Owner { get; set; }

        /// <summary>
        /// 其他属性
        /// </summary>
        [DataMember]
        public Dictionary<String, String> KeyTags { get; set; }
    }

    [MessageContract]
    public class ResoureMetadataMessage
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
        /// 修订号
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public long RevisionNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 归属人
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public String Owner { get; set; }

        /// <summary>
        /// 其他属性
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public Dictionary<String, String> KeyTags { get; set; }
    }

    [DataContract(IsReference = true)]
    public class FileCriteria
    {
        /// <summary>
        /// 文件名全称
        /// </summary>
        [DataMember]
        public string FileName { get; set; }

        /// <summary>
        /// 文件内容
        /// </summary>
        [DataMember]
        public string FileContent { get; set; }

        /// <summary>
        /// 浏览目录
        /// </summary>
        [DataMember]
        public string Catalog { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime LastModifiedTimeBegin { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime LastModifiedTimeEnd { get; set; }

        /// <summary>
        /// 归属人
        /// </summary>
        [DataMember]
        public String Owner { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        [DataMember]
        public Dictionary<String, String> KeyTags { get; set; }
    }
}