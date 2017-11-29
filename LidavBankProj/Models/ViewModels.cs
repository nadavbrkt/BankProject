using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LidavBankProj.Models
{
    public class VInOutTransaction
    {
        public List<TransactionModel> incoming { get; set; }
        public List<TransactionModel> outgoing { get; set; }
    }
}