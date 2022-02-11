using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenPráctico.Models
{
    public class CreateAcountViewModel
    {
        public CreateSavingAccountModel AccountModel { get; set; }

        public ClientModel Client { get; set; }
    }
}