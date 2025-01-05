using System;
using System.Collections.Generic;
using System.Linq;
namespace HamroPaisa.HamroPaisa.Models
{
    public class PaisaModel {
        
        public string Description {  get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPaid {  get; set; }
        public bool UnPaid { get; set; }
        public string Category { get; set; }

    }


}
