using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Aliyun.OSS;

namespace Sintoacct.Ledger.Common
{
    public class AliyunOss
    {
        private OssClient _oss;
        private const string ImageBucket = "sintoacct-progress-image";

        public AliyunOss()
        {
            _oss = new OssClient(AliyunOssConfig.Endpoint, AliyunOssConfig.AccessKeyId, AliyunOssConfig.AccessKeySecret);
        }

        public void PutObject(string key, Stream fileContent)
        {
            _oss.PutObject(ImageBucket, key, fileContent);
        }
    }
}