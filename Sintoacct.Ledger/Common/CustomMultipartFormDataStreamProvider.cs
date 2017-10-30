using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

namespace Sintoacct.Ledger.Common
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public string Root { get; set; }


        public CustomMultipartFormDataStreamProvider(string root):base(root)
        {
            Root = root;
        }


        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            string filePath = headers.ContentDisposition.FileName;

            // Multipart requests with the file name seem to always include quotes.
            if (filePath.StartsWith(@"""") && filePath.EndsWith(@""""))
                filePath = filePath.Substring(1, filePath.Length - 2);

            var filename = Path.GetFileName(filePath);
            var extension = Path.GetExtension(filePath);
            var contentType = headers.ContentType.MediaType;

            return filename;
        }
    }
}