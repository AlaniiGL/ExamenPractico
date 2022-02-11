using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenPráctico.Models
{
    public class TransactionViewModel
    {
        public List<TransactionModel> transactionList { get; set; }

        public ClientModel Client { get; set; }

        public bool IsCustom { get; set; }

    }
}