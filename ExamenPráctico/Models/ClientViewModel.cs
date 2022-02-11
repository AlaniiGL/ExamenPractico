using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenPráctico.Models
{
    public class ClientViewModel
    {
        public List<ClientModel> ClientModelList { get; set; }

        public string SearchQuery { get; set; }
    }
}