using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExamenPráctico.Models
{
    public class CreateSavingAccountModel
    {
        public ulong Id_Account { get; set; }

        public ulong Id_Client { get; set; }

        [Required(ErrorMessage = "Cantidad de depósito requerida")]
        [Display(Name = "Monto de la cuenta")]
        public ulong Account_Balance { get; set; }

        //Not in view
        public DateTime AccountCreateDate { get; set; }

        public DateTime AccountUpdateDate { get; set; }
    }
}