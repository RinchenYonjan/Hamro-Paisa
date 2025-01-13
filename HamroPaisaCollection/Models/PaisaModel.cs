using System;
using System.ComponentModel.DataAnnotations;


namespace HamroPaisa.HamroPaisaCollection.Models
{

    public class PaisaModel
    {
        [Key]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public decimal Dept { get; set; }
        public decimal Saving { get; set; }

    
        private PaisaModel PaisaModelObject = new PaisaModel();


    }

}




  

           
