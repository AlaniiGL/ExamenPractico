using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExamenPráctico.Models
{
    public class ClientModel
    {
        [Display(Name = "Id")]
        public ulong Id_Client { get; set; }

        [Required(ErrorMessage = "Nombre requerido")]
        [Display(Name = "Nombre")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Apellido Paterno Requerido")]
        [Display(Name = "Apellido Paterno")]
        public string ClientLastName1 { get; set; }

        [Required(ErrorMessage = "Apellido Materno Requerido")]
        [Display(Name = "Apellido Materno")]
        public string ClientLastName2 { get; set; }

        //Attributes not in form

        public DateTime ClientCreateDate { get; set; }

        public DateTime ClientUpdateDate { get; set; }

        public string SearchQuery { get; set; }
    }
}