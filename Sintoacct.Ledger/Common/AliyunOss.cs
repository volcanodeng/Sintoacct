using System;
using System.IO;
using System.Threading;
using Aliyun.OSS;

namespace Sintoacct.Ledger.Common
{
    public class AliyunOss
    {
        private OssClient _oss;
        private const string ImageBucket = "sintoacct-progress-image";
        private AutoResetEvent _event;

        public AliyunOss()
        {
            _oss = new OssClient(AliyunOssConfig.Endpoint, AliyunOssConfig.AccessKeyId, AliyunOssConfig.AccessKeySecret);

            _event = new AutoResetEvent(false);
        }

        public void PutObject(string key, Stream fileContent)
        {
            _oss.PutObject(ImageBucket, key, fileContent);
        }

        public void AsyncPutObject(string key, Stream fileContent)
        {
            _oss.BeginPutObject(ImageBucket, key, fileContent, PutObjectCallback, "sintoacct");

            _event.WaitOne();
        }

        private void PutObjectCallback(IAsyncResult ar)
        {
            try
            {
                var result = _oss.EndPutObject(ar);
            }
            catch(Exception err)
            {
                throw err;
            }
            finally
            {
                _event.Set();
            }
        }
    }
}