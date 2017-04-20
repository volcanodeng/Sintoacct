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

        public string SaveUpload(string fileName,string path,int fileSize)
        {
            return "";
        }
    }
}