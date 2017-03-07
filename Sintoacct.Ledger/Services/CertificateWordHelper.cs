using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sintoacct.Ledger.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace Sintoacct.Ledger.Services
{
    public class CertificateWordHelper 
    {
        private ClaimsIdentity _identity;
        private readonly LedgerContext _ledger;

        public CertificateWordHelper(HttpContextBase context, LedgerContext ledger)
        {
            _identity = context.User.Identity as ClaimsIdentity;
            _ledger = ledger;
        }

        public List<CertificateWord> GetCertWordInAccountBook()
        {
            List<CertificateWord> certWords = _ledger.Database.SqlQuery<CertificateWord>("select cw.* from T_Certificate_Word cw,T_Account_Book ab,T_User_Book ub where cw.AbId=ab.AbId and ab.AbId=ub.AbId and ub.UserId=@userid", _identity.GetUserId()).ToList();
            return certWords;
        }

        public int Save(CertWordViewModel certWord)
        {
            if(certWord.CwId >0)
            {
                CertificateWord cw = _ledger.CertificateWords.Where(c => c.CwId == certWord.CwId).FirstOrDefault();
                if(cw!= null)
                {
                    cw.CertWord = certWord.CertWord;
                    cw.PrintTitle = certWord.PrintTitle;
                    cw.IsDefault = certWord.IsDefault;
                }
            }
            else
            {
                CertificateWord newWord = new CertificateWord();
                newWord.CertWord = certWord.CertWord;
                newWord.PrintTitle = certWord.PrintTitle;
                newWord.IsDefault = certWord.IsDefault;

                var val = _identity.Claims.Where(c => c.Type == Constants.ClaimAccountBookID).Select(c => c.Value).FirstOrDefault();
                newWord.AccountBook = _ledger.AccountBooks.Where(ab => ab.AbId == Guid.Parse(val)).FirstOrDefault();

                _ledger.CertificateWords.Add(newWord);
            }

            return _ledger.SaveChanges();
        }
    }
}