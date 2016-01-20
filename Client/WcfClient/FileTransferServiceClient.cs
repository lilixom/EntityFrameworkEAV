using Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace Client.WcfClient
{
    public class FileTransferServiceClient : IFileTransferService
    {
        private static readonly ChannelFactory<IFileTransferService> _channelFactory;

        static FileTransferServiceClient()
        {
            _channelFactory = new ChannelFactory<IFileTransferService>("IFileService");
        }

        public RemoteFileInfo GetFile(GetRequest request)
        {
            return _channelFactory.OpenConnector(client => client.GetFile(request));
        }

        public long DownloadFile(ref string FileName, out System.IO.Stream FileByteStream)
        {
            GetRequest inValue = new GetRequest();
            inValue.Id = FileName;
            RemoteFileInfo retVal = ((IFileTransferService)(this)).GetFile(inValue);
            FileName = retVal.FileName;
            FileByteStream = retVal.FileByteStream;
            return retVal.Length;
        }

        public ResoureMetadataMessage SaveFile(ResoureMessage request)
        {
            return _channelFactory.OpenConnector(client => client.SaveFile(request));
        }

        public ResoureMetadataMessage SaveFile(string FileName, long Length, System.IO.Stream FileByteStream)
        {
            ResoureMessage inValue = new ResoureMessage();
            inValue.FileName = FileName;
            inValue.FileSize = Length;
            inValue.FileContent = FileByteStream;
            inValue.KeyTags = new Dictionary<string, string>();
            inValue.KeyTags.Add("k1", "v1");
            ResoureMetadataMessage retValue = ((IFileTransferService)(this)).SaveFile(inValue);
            return retValue;
        }

        public List<ResoureMetadataDTO> GetFileHistoryInfo(string Id)
        {
            throw new NotImplementedException();
        }

        public ResoureMetadataDTO GetFile(string id)
        {
            throw new NotImplementedException();
        }

        public System.IO.Stream GetFileContent(string id)
        {
            throw new NotImplementedException();
        }

        public void DeleteFile(string id)
        {
            throw new NotImplementedException();
        }

        public void DeleteFiles(FileCriteria condition)
        {
            throw new NotImplementedException();
        }

        public List<ResoureMetadataDTO> GetFiles(FileCriteria condition)
        {
            throw new NotImplementedException();
        }

        public ResoureMetadataDTO CopyFile(string id, ResoureMetadataDTO destFile)
        {
            throw new NotImplementedException();
        }
    }

    internal static class ConnectorExtension
    {
        public static void OpenConnector<T>(this ChannelFactory<T> channelFactory, Action<T> func)
        {
            var channel = channelFactory.CreateChannel();
            var commObj = channel as ICommunicationObject;
            try
            {
                func(channel);
                if (commObj.State != CommunicationState.Faulted)
                {
                    commObj.Close();
                }
            }
            finally
            {
                if (commObj.State != CommunicationState.Closed)
                {
                    commObj.Abort();
                }
            }
        }

        public static TU OpenConnector<T, TU>(this ChannelFactory<T> channelFactory, Func<T, TU> func)
        {
            var channel = channelFactory.CreateChannel();
            var commObj = channel as ICommunicationObject;
            try
            {
                var ret = func(channel);
                if (commObj.State != CommunicationState.Faulted)
                {
                    commObj.Close();
                }
                return ret;
            }
            finally
            {
                if (commObj.State != CommunicationState.Closed)
                {
                    commObj.Abort();
                }
            }
        }
    }
}