using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExamenPráctico.Models
{
    public class ShowAccountDetailsModel
    {
        public ulong Id_Client { get; set; }

        public ulong Id_Account { get; set; }

        public string ClientName { get; set; }

        public string ClientLastName1 { get; set; }

        public string ClientLastName2 { get; set; }

        public DateTime AccountCreateDate { get; set; }

        public ulong Account_Balance { get; set; }

        public ulong Transaction { get; set; }

        [Display(Name = "Monto")]
        [Required(ErrorMessage = "Monto requerido")]
        public ulong TransactionAmount { get; set; }

        public string TransactionKind { get; set; }
    }
}