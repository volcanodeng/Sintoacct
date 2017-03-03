using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Sintoacct.Ledger.Controllers.Api
{
    public class AccountBookApiController : ApiController
    {
        private readonly IAccountBookHelper _acctBook;

        public AccountBookApiController(IAccountBookHelper acctBook)
        {
            _acctBook = acctBook;
        }

        [HttpGet,ActionName("MyAcctBook")]
        [Route("acctbook/MyAcctBook")]
        public IHttpActionResult GetMyAccountBook()
        {
            return Ok(_acctBook.GetBooksOfUser());
        }

        [HttpPost,Route("acctbook/save")]
        public IHttpActionResult SaveAccountBook()
        {
            return null;
        }
    }
}