using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sintoacct.Ledger.Models;

namespace Sintoacct.Ledger.Services
{
    public class SourceDocumentHelper
    {
        private readonly LedgerContext _ledger;

        public SourceDocumentHelper(LedgerContext ledger)
        {
            _ledger = ledger;
        }

        public string SaveUpload(string sourceFileName,string relatePath,int fileSize)
        {
            SourceDocument uploadFile = new SourceDocument();
            uploadFile.FileId = Guid.NewGuid();
            uploadFile.SourceFileName = sourceFileName;
            uploadFile.RelateFileName = string.Format("{0}{1}", relatePath, sourceFileName);
            uploadFile.RelatePath = relatePath;
            uploadFile.FileSize = fileSize;

            return "";
        }
    }
}