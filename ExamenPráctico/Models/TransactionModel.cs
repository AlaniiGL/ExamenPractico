using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenPráctico.Models
{
    public class TransactionModel
    {
        public ulong Id_Transaction { get; set; }

        public ulong Id_Account { get; set; }

        public string FullName { get; set; }

        public string TransactionName { get; set; }

        public ulong Transaction_Quantity { get; set; }

        public ulong Previous_Balance { get; set; }

        public ulong Subsecuent_Balance { get; set; }

        public DateTime Transaction_Date { get; set; }
    }
}