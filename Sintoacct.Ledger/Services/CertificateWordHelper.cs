using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sintoacct.Ledger.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace Sintoacct.Ledger.Services
{
    public class CertificateWordHelper : ICertificateWordHelper
    {
        private readonly ClaimsIdentity _identity;
        private readonly LedgerContext _ledger;
        private readonly HttpContextBase _context;
        private readonly ICacheHelper _cache;

        public CertificateWordHelper(HttpContextBase context, LedgerContext ledger,ICacheHelper cache)
        {
            _identity = context.User.Identity as ClaimsIdentity;
            _ledger = ledger;
            _context = context;
            _cache = cache;
        }

        public List<CertificateWord> GetCertWordInAccountBook()
        {
            System.Data.SqlClient.SqlParameter PUserId = new System.Data.SqlClient.SqlParameter("@userid", _identity.GetUserId());
            List<CertificateWord> certWords = _ledger.Database.SqlQuery<CertificateWord>("select cw.* from T_Certificate_Word cw,T_Account_Book ab,T_User_Book ub where cw.AbId=ab.AbId and ab.AbId=ub.AbId and ub.UserId=@userid", PUserId).ToList();
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
                    //cw.IsDefault = certWord.IsDefault;
                    this.SetDefault(new CertWordViewModel() {CwId=cw.CwId,IsDefault=certWord.IsDefault });
                }
            }
            else
            {
                CertificateWord newWord = new CertificateWord();
                newWord.CertWord = certWord.CertWord;
                newWord.PrintTitle = certWord.PrintTitle;
                newWord.IsDefault = false;

                Guid abid;
                string val = _cache.GetUserCache().AccountBookID;//_identity.Claims.Where(c => c.Type == Constants.ClaimAccountBookID).Select(c => c.Value).FirstOrDefault();
                if (string.IsNullOrEmpty(val) || !Guid.TryParse(val, out abid)) throw new Exception("未绑定所属账套");
                newWord.AccountBook = _ledger.AccountBooks.Where(ab => ab.AbId == abid).FirstOrDefault();

                _ledger.CertificateWords.Add(newWord);
            }

            return _ledger.SaveChanges();
        }

        public int Delete(CertWordViewModel certWord)
        {
            CertificateWord cWord = _ledger.CertificateWords.Where(cw=>cw.CwId==certWord.CwId).FirstOrDefault();

            if(cWord != null)
            {
                _ledger.CertificateWords.Remove(cWord);
                return _ledger.SaveChanges();
            }
            return 0;
        }

        public int SetDefault(CertWordViewModel certWord)
        {
            CertificateWord cWord = _ledger.CertificateWords.Where(cw => cw.CwId == certWord.CwId).FirstOrDefault();
            List<CertificateWord> certWords = this.GetCertWordInAccountBook();
            if (cWord != null && certWord.IsDefault)
            {
                var defWord = certWords.Where(cw=>cw.IsDefault).FirstOrDefault();
                defWord.IsDefault = false;
                cWord.IsDefault = true;

                return _ledger.SaveChanges(); ;
            }
            return 0;
        }
    }

    public interface ICertificateWordHelper : IDependency
    {
        List<CertificateWord> GetCertWordInAccountBook();

        int Save(CertWordViewModel certWord);

        int Delete(CertWordViewModel certWord);

        int SetDefault(CertWordViewModel certWord);
    }
}