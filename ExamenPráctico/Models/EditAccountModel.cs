using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenPráctico.Models
{
    public class EditAccountModel
    {
        public ulong Id_Account { get; set; }

        //public ulong Id_Client { get; set; }

        public ulong TransactionAmount { get; set; }

        public ulong Amount { get; set; }

        public ulong Id_TransactionType { get; set; }

        public ulong Previous_Balance { get; set; }
    }
}